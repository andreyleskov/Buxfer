using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Buxfer.Client.Responses;
using Buxfer.Client.Security;
using Buxfer.Client.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;

namespace Buxfer.Client
{
    /// <summary>
    ///     Buxfer API client.
    /// </summary>
    public sealed class BuxferClient
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BuxferClient" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="logger">The logger. Could be ignored to disable any log output</param>
        /// TODO: rework authentication
        public BuxferClient(string userName, string password, ILogger logger = null)
        {
            _logger = logger ?? new NullLogger<BuxferClient>();

            var authenticator = new TokenAuthenticator(userName, password,
                (resource, method) => CreateRequestBuilder(resource, method).Request,
                async r => await ExecuteRequestAsync<LoginResponse>(r), _logger);

            Init(authenticator);
        }

        public BuxferClient(string token, ILogger logger = null)
        {
            _logger = logger ?? new NullLogger<BuxferClient>();
            Init(new PresetTokenAuthenticator(token));
        }


        private void Init(ITokenAuthenticator authenticator)
        {
            ApiBaseUrl = "https://www.buxfer.com/api/";
            _restClient = new RestClient(ApiBaseUrl);
            _restClient.AddHandler("application/x-javascript", () => new JsonDeserializer());
            _restClient.PreAuthenticate = true;
            _restClient.Authenticator = authenticator;
            _authenticator = authenticator;
        }

        #endregion

        public async Task<string> Login()
        {
            await GetTransactions();
            return _authenticator.Token;
        }

        #region Fields

        private ITokenAuthenticator _authenticator;
        private IRestClient _restClient;
        private readonly ILogger _logger;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the API base URL.
        /// </summary>
        /// <value>
        ///     The API base URL.
        /// </value>
        public string ApiBaseUrl { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="BuxferClient" /> is authenticated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool Authenticated => _authenticator.Authenticated;

        #endregion

        #region Methods

        /// <summary>
        ///     Adds expense or income transaction.
        ///     Use an overload with creation request instead
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>True if transaction was added.</returns>
        [Obsolete]
        public async Task<bool> AddTransaction(Transaction transaction)
        {
            var serializer = new TransactionSerializer(transaction);
            var builder = CreateRequestBuilder("add_transaction", Method.POST);
            var request = builder.Request;
            request.AddParameter("format", "sms", ParameterType.GetOrPost);
            request.AddParameter("text", serializer.SerializeToSmsText());

            var executeRequestAsync = await ExecuteRequestAsync<AddTransactionResponse>(request);
            return executeRequestAsync.TransactionAdded;
        }

        private async Task<T> AddTransaction<T>(Transaction transaction,
            Action<IRestRequest> additionalInit = null,Func<RawTransaction,T> customTransactionBuilder=null)
        {
            var builder = CreateRequestBuilder("add_transaction", Method.POST);
            var request = builder.Request;
            AddCreationRequest(request, transaction);
            additionalInit?.Invoke(request);
            if(customTransactionBuilder==null)
                return await ExecuteRequestAsync<T>(request);

            var rawTransaction = await ExecuteRequestAsync<RawTransaction>(request);
            return customTransactionBuilder(rawTransaction);
        }

        /// <summary>
        ///     Adds expense transaction 
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Transaction creation status</returns>
        public async Task<ExpenseTransaction> AddTransaction(ExpenseTransaction transaction)
        {
            return await AddTransaction<ExpenseTransaction>(transaction, null);
        }

        /// <summary>
        ///     Adds income transaction
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Transaction creation status</returns>
        public async Task<IncomeTransaction> AddTransaction(IncomeTransaction transaction)
        {
            return await AddTransaction<IncomeTransaction>(transaction, null);
        }

        /// <summary>
        ///     Adds transfer transaction
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Transaction creation status</returns>
        public async Task<TransferTransaction> AddTransaction(TransferTransaction transaction)
        {
            return await AddTransaction<TransferTransaction>(transaction, b =>
            {
                b.AddIfNotZero("fromAccountId", transaction.FromAccountId);
                b.AddIfNotZero("toAccountId", transaction.ToAccountId);
            },raw => raw.ToTransfer());
        }

        /// <summary>
        ///     Adds refund transaction
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Transaction creation status</returns>
        public async Task<RawTransaction> AddTransaction(RefundCreationTransaction transaction)
        {
            return await AddTransaction<RawTransaction>(transaction, null);
        }

        /// <summary>
        ///     Adds loan transaction
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Transaction creation status</returns>
        public async Task<RawTransaction> AddTransaction(LoanTransaction transaction)
        {
            return await AddTransaction<RawTransaction>(transaction, r =>
            {
                r.AddIfNotEmpty("borrowedBy", transaction.BorrowedBy);
                r.AddIfNotEmpty("loanedBy", transaction.LoanedBy);
            });
        }

        /// <summary>
        ///     Adds shared bill transaction
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Transaction creation status</returns>
        public async Task<RawTransaction> AddTransaction(SharedBillCreationTransaction transaction)
        {
            return await AddTransaction<RawTransaction>(transaction, r =>
            {
                r.AddIfNotEmpty("payers", transaction.Payers);
                r.AddIfNotEmpty("sharers", transaction.Sharers);
                r.AddParameter("isEvenSplit", transaction.IsEvenSplit);
            });
        }

        /// <summary>
        ///     Adds paid for friend transaction
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Transaction creation status</returns>
        public async Task<RawTransaction> AddTransaction(PaidForFriendTransaction transaction)
        {
            return await AddTransaction<RawTransaction>(transaction, r =>
            {
                r.AddIfNotEmpty("paidFor", transaction.PaidFor);
                r.AddIfNotEmpty("paidBy", transaction.PaidBy);
            });
        }


        private void AddCreationRequest(IRestRequest request, Transaction transaction)
        {
            request.AddIfNotEmpty("description", transaction.Description);
            request.AddParameter("amount", transaction.Amount);
            request.AddIfNotZero("accountId", transaction.AccountId);
            request.AddParameter("date", transaction.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            request.AddIfNotEmpty("tags", transaction.Tags);
            request.AddParameter("type", transaction.Type.ToApiString());
            request.AddParameter("status", transaction.Status.ToString().ToLower());
        }

        /// <summary>
        ///     Uploads the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>True if statement was uploaded.</returns>
        public async Task<bool> UploadStatement(Statement statement)
        {
            var builder = CreateRequestBuilder("upload_statement", Method.POST);
            var request = builder.Request;
            request.AddParameter("accountId", statement.AccountId, ParameterType.GetOrPost);
            request.AddParameter("statement", statement.Text, ParameterType.GetOrPost);

            if (!string.IsNullOrEmpty(statement.DateFormat))
                request.AddParameter("dateFormat", statement.DateFormat, ParameterType.GetOrPost);

            try
            {
                var response = await ExecuteRequestAsync<UploadStatementResponse>(request);
                return response.Uploaded;
            }
            catch (BuxferException ex)
            {
                if (ex.Message.Equals("Invalid request", StringComparison.OrdinalIgnoreCase)) return false;
                throw;
            }
        }

        /// <summary>
        ///     Gets the transactions.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The transactions.</returns>
        public async Task<FilterResult<Transaction>> GetTransactions(TransactionFilter filter = null)
        {
            var response = await ExecuteGetAsync<TransactionsResponse>("transactions", filter);
            return new FilterResult<Transaction>(response.Transactions, response.NumTransactions);
        }

        /// <summary>
        ///     Gets the transactions.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The transactions.</returns>
        public async Task<FilterResult<Transaction>> GetTransactions(Action<TransactionFilter> filter)
        {
            var transactionFilter = new TransactionFilter();
            filter(transactionFilter);
            return await GetTransactions(transactionFilter);
        }

        /// <summary>
        ///     Gets the transactions.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The transactions.</returns>
        public async Task<FilterResult<RawTransaction>> GetExtendedTransactions(Action<TransactionFilter> filter)
        {
            var transactionFilter = new TransactionFilter();
            filter(transactionFilter);
            var response = await ExecuteGetAsync<ExtendedTransactionResponse>("transactions", filter);
            return new FilterResult<RawTransaction>(response.Transactions, response.NumTransactions);
        }

        /// <summary>
        ///     Gets the accounts.
        /// </summary>
        /// <returns>The accounts.</returns>
        public async Task<IReadOnlyCollection<Account>> GetAccounts()
        {
            var response = await ExecuteGetAsync<AccountsResponse>("accounts", null);
            return response.Accounts;
        }

        /// <summary>
        ///     Gets the loans.
        /// </summary>
        /// <returns>The loans.</returns>
        public async Task<IReadOnlyCollection<Loan>> GetLoans()
        {
            var response = await ExecuteGetAsync<LoansResponse>("loans", null);
            return response.Loans.Select(t => t.KeyLoan).ToArray();
        }

        /// <summary>
        ///     Gets the tags.
        /// </summary>
        /// <returns>The tags.</returns>
        public async Task<IReadOnlyCollection<Tag>> GetTags()
        {
            var response = await ExecuteGetAsync<TagsResponse>("tags", null);
            return response.Tags;
        }

        /// <summary>
        ///     Gets the budgets.
        /// </summary>
        /// <returns>The budgets.</returns>
        public async Task<IReadOnlyCollection<Budget>> GetBudgets()
        {
            var response = await ExecuteGetAsync<BudgetsResponse>("budgets", null);
            return response.Budgets;
        }

        /// <summary>
        ///     Gets the reminders.
        /// </summary>
        /// <returns>The reminders.</returns>
        public async Task<IReadOnlyCollection<Reminder>> GetReminders()
        {
            var response = await ExecuteGetAsync<RemindersResponse>("reminders", null);
            return response.Reminders;
        }

        /// <summary>
        ///     Gets the groups.
        /// </summary>
        /// <returns>The groups.</returns>
        public async Task<IReadOnlyCollection<Group>> GetGroups()
        {
            var response = await ExecuteGetAsync<GroupsResponse>("groups", null);
            return response.Groups;
        }

        /// <summary>
        ///     Gets the contacts.
        /// </summary>
        /// <returns>The contacts.</returns>
        public async Task<IReadOnlyCollection<Contact>> GetContacts()
        {
            var response = await ExecuteGetAsync<ContactsResponse>("contacts", null);
            return response.Contacts;
        }

        #endregion

        #region Private methods

        private static RequestBuilder CreateRequestBuilder(string resource, Method method)
        {
            var request = new RestRequest(resource, method);

            return new RequestBuilder(request);
        }


        private async Task<TResponse> ExecuteGetAsync<TResponse>(string resource, object filter)
            where TResponse : SuccessResponseBase
        {
            var requestBuilder = CreateRequestBuilder(resource, Method.GET);
            requestBuilder.Query(filter);

            return await ExecuteRequestAsync<TResponse>(requestBuilder.Request);
        }

        private async Task<TResponse> ExecuteRequestAsync<TResponse>(IRestRequest request)
        {
            var output = await _restClient.ExecuteAsync<Output<TResponse>>(request);
            if (output.StatusCode == HttpStatusCode.OK && output.Data != null) return output.Data.Response;
            if (output.Data == null) throw new BuxferException(output.ErrorMessage);

            throw new BuxferException(output.Data.Error.Message);
        }

        #endregion
    }
}