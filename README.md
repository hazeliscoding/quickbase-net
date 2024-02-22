# QuickbaseNet 🚀 [![NuGet](https://img.shields.io/nuget/v/QuickbaseNet?label=NuGet&logo=nuget&style=flat-square)](https://www.nuget.org/packages/QuickbaseNet/)

## 📋 Overview

QuickbaseNet is a versatile C# library designed to simplify and streamline interactions with the QuickBase API. Tailored for developers looking to efficiently perform CRUD operations and build complex queries, QuickbaseNet offers a set of intuitive tools including `QuickBaseCommandBuilder`, `QueryBuilder`, and `QuickbaseClient`. Whether you're managing database records or crafting detailed queries, QuickbaseNet enhances your experience with QuickBase tables through its fluent and user-friendly interfaces.

## ✨ Features

- **Fluent Interface 🌊**: Engage with methods that allow for easy and intuitive construction of various requests.
- **Comprehensive CRUD Operations 🛠️**: Use `QuickBaseCommandBuilder` to add new records, update existing ones, or delete records with efficiency.
- **Enhanced Record Management 📈**: Improved `RecordBuilder` for more intuitive record modifications and additions.
- **Advanced Query Support 🔍**: Leverage `QueryBuilder` to construct complex query requests effortlessly.
- **Seamless Client Setup 🌐**: Initialize connections with `QuickbaseClient`, providing a secure and straightforward way to interact with the API.

## 💾 Installation

Get started with QuickbaseNet by installing it via NuGet or cloning the repository:

```bash
# Install via NuGet
Install-Package QuickbaseNet

# Or clone the repository
git clone https://github.com/ducksoop/quickbase-net.git
```

## 🛠️ Usage

QuickbaseNet simplifies QuickBase API interactions. Below are examples showcasing its main features:

### Initializing QuickbaseClient 🌟

```csharp
// Initialize QuickbaseClient with your realm hostname and user token
var quickbaseClient = new QuickbaseClient("your_realm_hostname", "your_user_token");
```

### Handling API Responses 📬

#### Inserting Records

```csharp
// Use QuickBaseCommandBuilder to configure and build an insert request
var insertRequest = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    .ReturnFields(1, 2, 3)
    .AddNewRecord(record => record
        .AddFields(
            (6, "New record description"),
            (7, 100),
            (9, "2024-02-13"))
    )
    .BuildInsertUpdateCommand();

// Send the request and handle the response
var result = await quickbaseClient.InsertRecords(insertRequest);

if (result.IsSuccess) {
    // Success logic
} else {
    // Error handling
}
```

#### Updating Records

```csharp
// Configure and build an update request with QuickBaseCommandBuilder
var updateRequest = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    .ReturnFields(1, 2, 3) // Specify which fields to return after the update operation
    .UpdateRecord(8, record => record // Specify the record to update based on its record ID (8 in this example)
        .AddField(7, 150) // Update field 7 with a new value
        .AddField(9, "2024-02-15")) // Update field 9 with a new value
    .BuildInsertUpdateCommand();    .BuildInsertUpdateCommand();

// Send the request and handle the response
var result = await quickbaseClient.UpdateRecords(updateRequest);

if (result.IsSuccess) {
    // Success logic
} else {
    // Error handling
}
```

#### Deleting Records

```csharp
// Build and send a delete request with QuickBaseCommandBuilder
var deleteRequest = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    .WithDeletionCriteria("{6.EX.'hello'}")
    .BuildDeleteCommand();

// Process the response
var result = await quickbaseClient.DeleteRecords(deleteRequest);

if (result.IsSuccess) {
    // Success logic
} else {
    // Error handling
}
```

### QueryBuilder - Precision in Crafting Queries 🔎

#### Building and Sending a Query 📤

```csharp
// Construct a query with QueryBuilder
var query = new QueryBuilder()
    .From("bck7gp3q2")
    .Select(1, 2, 3)
    .Where("{1.CT.'hello'}")
    .SortBy(4, "ASC")
    .SortBy(5, "ASC")
    .GroupBy(6, "equal-values")
    .Build();

// Execute the query and process the response
var result = await quickbaseClient.QueryRecords(query);

if (result.IsSuccess) {
    // Success logic
} else {
    // Error handling
}
```

## 👐 Contributing

Contributions are

 greatly appreciated and help make the open-source community an amazing place to learn, inspire, and create. Feel free to contribute!

## 📜 License

Distributed under the MIT License. See [LICENSE](https://github.com/ducksoop/quickbase-net/blob/master/LICENSE.txt) for more information.

## 📚 Additional Resources

- [QuickBase API Documentation](https://developer.quickbase.com)
