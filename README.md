# QuickbaseNet 🚀

## 📋 Overview

QuickbaseNet is a versatile C# library designed to simplify and streamline interactions with the QuickBase API. Tailored for developers looking to efficiently perform CRUD operations and build complex queries, QuickbaseNet offers a set of intuitive tools including `QuickBaseCommandBuilder`, `QueryBuilder`, and `QuickbaseClient`. Whether you're managing database records or crafting detailed queries, QuickbaseNet enhances your experience with QuickBase tables through its fluent and user-friendly interfaces.

## ✨ Features

- **Fluent Interface 🌊**: Methods for building various requests easily and intuitively.
- **Comprehensive CRUD Operations 🛠️**: `QuickBaseCommandBuilder` for adding new records, updating existing ones, or deleting records efficiently.
- **Advanced Query Support 🔍**: `QueryBuilder` for constructing complex query requests with ease.
- **Seamless Client Setup 🌐**: `QuickbaseClient` for initializing connections with realm and user token for secure and straightforward API interaction.

## 💾 Installation

To get started with QuickbaseNet, you can install it via NuGet or clone the repository:

```bash
# Install via NuGet
Install-Package QuickbaseNet

# Or clone the repository
git clone https://github.com/yourusername/quickbase-net.git
```

## 🛠️ Usage

QuickbaseNet simplifies working with the QuickBase API across various operations. Here's how you can use its main features:

### Initializing QuickbaseClient 🌟

```csharp
// Initialize QuickbaseClient with your realm hostname and user token
var quickbaseClient = new QuickbaseClient("your_realm_hostname", "your_user_token");
```

<!--
### Handling API Responses 📬

#### Sending a Query Request

```csharp
// Build a query using QueryBuilder
var query = new QueryBuilder()
    .From("bck7gp3q2")
    .Select(1, 2, 3)
    .Where("{1.CT.'hello'}")
    .Build();

// Send the query and handle the response
var (response, error, isSuccess) = await quickbaseClient.QueryRecords(query);

if (isSuccess)
{
    // Process the successful response
}
else
{
    // Handle the error
}
```

#### Inserting Records

```csharp
// Configure and build an insert request using QuickBaseCommandBuilder
var insertRequest = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    // Add configuration for insert request...
    .BuildInsertOrUpdateRequest();

// Send the insert request and handle the response
var (response, error, isSuccess) = await quickbaseClient.InsertRecords(insertRequest);

if (isSuccess)
{
    // Handle successful insert response
}
else
{
    // Process the error
}
```

#### Updating Records

```csharp
// Configure and build an update request
var updateRequest = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    // Add configuration for update request...
    .BuildInsertOrUpdateRequest();

// Send the update request and handle the response
var (response, error, isSuccess) = await quickbaseClient.UpdateRecords(updateRequest);

if (isSuccess)
{
    // Handle successful update response
}
else
{
    // Process the error
}
```

#### Deleting Records

```csharp
// Build a delete request using QuickBaseCommandBuilder
var deleteRequest = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    .WithDeletionCriteria("{6.EX.'hello'}")
    .BuildDeleteRequest();

// Send the delete request and handle the response
var response = await quickbaseClient.DeleteRecords(deleteRequest);
```
##
-->
### QueryBuilder - Crafting Queries with Precision 🔎

#### Building and Sending a Query 📤

```csharp
// Create a query using QueryBuilder
var query = new QueryBuilder()
    .From("bck7gp3q2")
    .Select(1, 2, 3)
    .Where("{1.CT.'hello'}")
    .SortBy(4, "ASC")
    .SortBy(5, "ASC")
    .GroupBy(6, "equal-values")
    .Build();

// Send the query and handle the response
var response = await quickbaseClient.QueryRecords(query);
```

## 👐 Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

## 📜 License

Distributed under the MIT License. See [LICENSE](https://github.com/ducksoop/quickbase-net/blob/master/LICENSE.txt) for more information.

## 📚 Additional Resources

- [QuickBase API Documentation](https://developer.quickbase.com)
