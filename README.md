Buxfer.Client is a .Net core library to access Buxfer.com REST API 
 
[![Build status](https://ci.appveyor.com/api/projects/status/qcg2efpqllydpcis/branch/master?svg=true)](https://ci.appveyor.com/project/ContextCore/buxfer/branch/master)

Install
===

get it at https://www.nuget.org/packages/Buxfer.Client/

Usage
===

```csharp
// Creating client.
Microsoft.Extensions.Logging.ILogger logger = <init somewhere>
var client = new BuxferClient("<your user>", "<your password>", logger);

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
reports = await client.GetReports(new ReportFilter() 
{
	AccountName = "My Bank",
	Tag = "Car"
});

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
var transaction = new Transaction()
{
	Description = "Test transaction",
	Amount = 1,
   	AccountName = "<account name>"
};

var added = client.AddTransaction(transaction);

```

Security
===
I do not like the idea of passing password to the client, 
but it is the only supported way of Buxfer API. 

Special thanks
===
Project has been created from https://github.com/giacomelli/BuxferSharp, adapting it to .Net Core and some minor improvements.


