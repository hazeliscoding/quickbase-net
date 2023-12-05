# QuickbaseNet 🚀

## 📋 Overview

This repository contains C# utilities designed to simplify constructing requests for the QuickBase API and provide intuitive interfaces for building insert, update, delete, and query operations on QuickBase tables.

## ✨ Features

- **Fluent Interface 🌊**: Methods for building various requests easily and intuitively.
- **Comprehensive CRUD Operations 🛠️**: `QuickBaseCommandBuilder` for adding new records, updating existing ones, or deleting records efficiently.
- **Advanced Query Support 🔍**: `QueryBuilder` for constructing complex query requests with ease.
- **Seamless Client Setup 🌐**: `QuickbaseClient` for initializing connections with realm and user token for secure and straightforward API interaction.

## 💾 Installation

Provide installation instructions, such as NuGet package installation, cloning a repository, or manually adding files.

```bash
# Example: NuGet Package Installation
Install-Package YourPackageName
```

## 🛠️ Setup

### QuickbaseClient Initialization 🌟

Initialize `QuickbaseClient` with your realm hostname and user token for a secure connection.

```csharp
var quickbaseClient = new QuickbaseClient("your_realm_hostname", "your_user_token");
```

### QuickBaseCommandBuilder Usage 🧩

#### Inserting Records 💾

```csharp
var commandBuilder = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    .AddNewRecord(record => record
        .AddField("fieldId1", "Value1")
        .AddField("fieldId2", "Value2"))
    .BuildInsertOrUpdateRequest();

// Send request using QuickbaseClient
var response = await quickbaseClient.InsertOrUpdateRecords(commandBuilder);
```

#### Updating Records 🔄

```csharp
var commandBuilder = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    .UpdateRecord(123, record => record
        .AddField("fieldId1", "New Value1")
        .AddField("fieldId2", "New Value2"))
    .BuildInsertOrUpdateRequest();

// Send request using QuickbaseClient
var response = await quickbaseClient.InsertOrUpdateRecords(commandBuilder);
```

#### Deleting Records ❌

```csharp
var commandBuilder = new QuickBaseCommandBuilder()
    .ForTable("your_table_id")
    .WithDeletionCriteria("{6.EX.'hello'}")
    .BuildDeleteRequest();

// Send request using QuickbaseClient
var response = await quickbaseClient.DeleteRecords(commandBuilder);
```

### QueryBuilder - Crafting Queries with Precision 🔎

#### Building and Sending a Query 📤

```csharp
var query = new QueryBuilder()
    .From("bck7gp3q2")
    .Select(1, 2, 3)
    .Where("{1.CT.'hello'}")
    .SortBy(4, "ASC")
    .SortBy(5, "ASC")
    .GroupBy(6, "equal-values")
    .Build();

// Send query using QuickbaseClient
var response = await quickbaseClient.QueryRecords(query);
```

## 👐 Contributing
Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

## 📜 License

Distributed under the MIT License. See [LICENSE](https://github.com/ducksoop/quickbase-net/blob/master/LICENSE.txt) for more information.
