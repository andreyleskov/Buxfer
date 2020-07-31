Buxfer.Client is a .Net core library to access Buxfer.com REST API 
 
[![Build status](https://ci.appveyor.com/api/projects/status/qcg2efpqllydpcis/branch/master?svg=true)](https://ci.appveyor.com/project/ContextCore/buxfer/branch/master)

Install
===

get it at https://www.nuget.org/packages/Buxfer.Client/

Usage
===

```csharp
// Creating client.
var client = new BuxferClient("<your user>", "<your password>");

// Getting all accounts.
var accounts = await client.GetAccounts();

// Getting all budgets.
var budgets = await client.GetBudgets();

// Getting all contacts.
var contacts = await client.GetContacts();

// Getting all groups.
var groups = await client.GetGroups();

// Getting all loans.
var loans = await client.GetLoans();

// Getting all reminders.
var reminders = await client.GetReminders();

// Getting all reports.
var reports = await client.GetReports();

// Getting reports by filter.
reports = await client.GetReports();

// Upload a statement.
var statement = new Statement();
statement.AccountId = "<account id>";
statement.Text = "<Quicken, MS Money, OFX, QIF, QFX, Excel, CSV file content>";
bool uploaded = await client.UploadStatement(statement);

// Getting all tags.
var tags = await client.GetTags();

// Getting last 25 transactions.
var lastTransactions = await client.GetTransactions();


// Getting last transactions from page 2.
var page2Transactions = await client.GetTransactions(new TransactionFilter() 
{
	Page = 2
});

// Add a transaction.
var transaction = new IncomeTransaction()
{
	Description = "Test transaction",
	Amount = 1,
   	AccountName = "<account name>"
};

var added = client.AddTransaction(transaction);

```

Supported transactions
===
[Buxfer API reference](https://www.buxfer.com/help/api) is limited about supported transaction parameters to create and 
transaction types to get in response. 
I had to construct some classes based on real responses from the API and not on the documentation. 
There are several transaction types available in Buxfer UI, but Buxfer.Client support only following: 
`Income, Expense,Transfer,Refund`
Limited support, not tested, but saw in API responses: `Loan, PaidForFriend,SharedBill`

Loading raw transactions
---
Buxfer.Client try to create an appropriate transaction class based on `type` when calling `client.GetTransactions()`
If you need an access to low-level API response, use `client.GetRawTransactions()` instead

Security
===
I do not like the idea of passing password to the client, 
but it is the only supported way of Buxfer API. 
An alternative is to get a token by calling login method, store it, and use later 
instead of password. Buxfer does not provide information about token lifetime. 
I used tokens for testing for a few days. 

```csharp
//Create client.
var client = new BuxferClient("<your user>", "<your password>");
var token = await client.Login();
//Store and load token
var clientFromToken = new BuxferClient(token);
```

User Secrets in Tests
---
`Buxfer.Client.Tests.Web` project contains tests calling Buxfer API. 
It needs some sensitive information like your account name, password, tags and account ids.
It is stored in [user secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows#enable-secret-storage) provided by .Net Core ecosystem
There is an [example](https://github.com/andreyleskov/Buxfer/blob/master/Buxfer.Client.Tests.Web/userSecretsExample.json) of secrets file structure. 

Special thanks
===
Project has been created from https://github.com/giacomelli/BuxferSharp, adapting it to .Net Core and some minor improvements.


