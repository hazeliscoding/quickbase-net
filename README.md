# QuickbaseNet

**Talk to Quickbase from .NET without hand-building JSON.** QuickbaseNet wraps Quickbase's JSON
API in fluent query and command builders. Every call returns a result you check, not an exception
you catch.

Quickbase's API addresses everything by numeric field ID. A query is a JSON body with a table ID,
a list of field IDs and a where clause in Quickbase's own query language, like `{6.EX.'hello'}`.
Records come back as dictionaries keyed by field ID, with every value wrapped in an object:
`{"6": {"value": "hello"}}`. Each request also needs your realm hostname and a user token in
custom headers. QuickbaseNet sets the headers once and builds the request bodies for you.

> **Status:** 1.0.2 on [NuGet](https://www.nuget.org/packages/QuickbaseNet/). It covers querying,
> inserting, updating and deleting records. Next is 1.1: paging, and the full results of inserts
> and updates. See [ROADMAP.md](ROADMAP.md).

## Install

```sh
dotnet add package QuickbaseNet
```

It targets .NET Standard 2.0 and 2.1, .NET Framework 4.8, .NET 5 and .NET 6, so it runs in older
.NET Framework apps as well as current .NET.

## Usage

```csharp
using QuickbaseNet.Helpers;
using QuickbaseNet.Services;

// Your realm is the part before .quickbase.com.
var client = new QuickbaseClient("your-realm", "your-user-token");
```

### Query records

```csharp
var query = new QuickbaseQueryBuilder()
    .From("bck7gp3q2")          // table ID
    .Select(3, 6, 7)            // field IDs to return
    .Where("{6.CT.'hello'}")    // field 6 contains "hello"
    .SortBy(7, "DESC")
    .GroupBy(6, "equal-values")
    .Build();

var result = await client.QueryRecords(query);

if (result.IsSuccess)
{
    foreach (var record in result.Value.Data)
    {
        var description = record["6"].GetValue<string>();
        var amount = record["7"].GetValue<decimal>();
    }
}
```

A query that matches nothing comes back as a `NotFound` failure, not an empty list.

### Insert and update records

```csharp
var command = new QuickbaseCommandBuilder()
    .ForTable("bck7gp3q2")
    .ReturnFields(3, 6, 7)
    .AddNewRecord(record => record
        .AddFields(
            (6, "New record"),
            (7, 100),
            (9, "2024-02-13")))
    .UpdateRecord(8, record => record    // record ID 8
        .AddField(7, 150))
    .BuildInsertUpdateCommand();

var result = await client.InsertRecords(command);
```

Inserts and updates can share one request, because both go to Quickbase's upsert endpoint.
`UpdateRecord` fills in the built-in Record ID# field (3) for you. `UpdateRecords` sends the same
request, so use whichever name reads better.

### Delete records

```csharp
var command = new QuickbaseCommandBuilder()
    .ForTable("bck7gp3q2")
    .WithDeletionCriteria("{6.EX.'hello'}")   // every record where field 6 is exactly "hello"
    .BuildDeleteCommand();

var result = await client.DeleteRecords(command);
```

### Handle errors

```csharp
if (result.IsFailure)
{
    var error = result.QuickbaseError;
    // error.Type is ClientError (4xx), ServerError (5xx) or NotFound.
    Console.WriteLine($"{error.Type}: {error.Message}");
}
```

Reading `result.Value` on a failure throws, so check `IsSuccess` first.

## What it does

- **Queries** with `QuickbaseQueryBuilder`: select, where, sort and group.
- **Inserts and updates** with `QuickbaseCommandBuilder`: several records per request and several
  fields per record, with typed values.
- **Deletes** every record that matches a where clause.
- **Returns results, not exceptions.** Each call returns a `QuickbaseResult<T>` with `IsSuccess`,
  `Value` and a typed `QuickbaseError`.
- **Sets up the client once.** The realm, user token and user agent go into the headers when you
  create the client.

## Development

You need the .NET 8 SDK.

```sh
dotnet build
dotnet test     # xUnit, against a mocked HTTP handler: no Quickbase account needed
```

CI builds and tests every push and pull request. Releases come from version tags: pushing
`v1.2.3` packs version 1.2.3 and publishes it to NuGet.

## Contributing

Issues and pull requests are welcome. The next unchecked item in [ROADMAP.md](ROADMAP.md) is
what's being built.

## License

[MIT](LICENSE.txt)

See also the [Quickbase API documentation](https://developer.quickbase.com).
