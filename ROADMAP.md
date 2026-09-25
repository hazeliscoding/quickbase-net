# Roadmap

QuickbaseNet is a .NET library for Quickbase's JSON API, published on NuGet. This file tracks what
gets built next, in what order, and the decisions already made.

## Decisions (2026-09-25)

- **Releases come from version tags.** Pushing `v1.2.3` packs version 1.2.3 and publishes it to
  NuGet. Pushes and pull requests only build and test. The `<Version>` in the csproj is just the
  default for local builds.
- **1.x stays compatible.** New behavior in 1.1 arrives as additions and new overloads. Anything
  that changes an existing signature or an existing result waits for 2.0.
- **Records first.** The library wraps the records API: query, insert, update and delete. Other
  endpoints get wrapped when someone needs one, not for completeness.
- **Test against the published spec, not a live account.** There's no Quickbase account for
  day-to-day work. Quickbase's OpenAPI definition, from developer.quickbase.com, stands in for the
  service: its examples are the test fixtures. The spec shows what the API is documented to do,
  so each release still gets one check against a real realm, through an opt-in smoke test.

## M1: 1.1, fill the gaps

- [ ] Renew the `NUGET_API_KEY` repo secret. The current one dates from January 2024, and NuGet
  keys expire within a year.
- [ ] Commit Quickbase's OpenAPI definition to the test project. Parse its example responses
  through the library's response types, and check the builders' request bodies against its
  request examples.
- [ ] Make `MockHttpMessageHandler` record every request, so tests can assert the path, method,
  `QB-Realm-Hostname` and `Authorization` headers, and the JSON body. Today it ignores the request.
- [ ] A constructor overload that takes an `HttpClient` and still sets the auth headers on it. It's
  an addition, so it fits 1.x. Tests use it instead of replacing `Client`, which skips the
  constructor's header setup.
- [ ] Hand-written fakes for what the spec's examples don't show: a query paged across three
  pages, an upsert with failed lines, a 429, an HTML 502 page and a timeout.
- [ ] Paging: `.Skip()` and `.Top()` on `QuickbaseQueryBuilder`. The request already has an
  `Options` field, but the builder never sets it.
- [ ] A `QueryAllRecords` method that keeps requesting pages until `metadata.totalRecords` is
  reached.
- [ ] Upsert results: model `createdRecordIds`, `updatedRecordIds`, `unchangedRecordIds`,
  `totalNumberOfRecordsProcessed` and `lineErrors` on the insert and update response. Today's
  `Metadata` type has the query fields instead, so all of these are dropped.
- [ ] Delete results: check what the delete endpoint returns and give it its own response type,
  instead of reusing the upsert one.
- [ ] Network failures and error bodies that aren't JSON (an HTML 502 page, say) come back as
  failures instead of throwing.
- [ ] `CancellationToken` on every client method, added as overloads so 1.0 callers keep working.
- [ ] Remove `Microsoft.AspNet.WebApi.Client`. It's referenced but never used.
- [ ] Build the user agent from the assembly version. It's hardcoded as `QuickbaseNet/1.0.1`.
- [ ] Tests for `InsertRecords`, `UpdateRecords`, `DeleteRecords` and both builders, including
  their error paths. Today's 7 tests cover the constructor, `QueryRecords` and the result type.
- [ ] An opt-in smoke test against a real realm: query, insert, update and delete in one table. It
  runs only when `QB_REALM`, `QB_TOKEN` and `QB_TABLE` are set, and skips otherwise.
- [ ] Run the smoke test against a real realm once, from a Quickbase trial or a user who has a
  realm.
- [ ] Document paging and upsert results in the README, then tag `v1.1.0`.

**Done when:** a query through the builder can read every record in a table larger than one page,
an upsert reports its created IDs and line errors, and no HTTP or network failure throws. All of it
is covered by tests built on the spec, and the smoke test has passed against a real realm.

## M2: 2.0, modernize

- [ ] Remove the settable `Client` property, so the `HttpClient` constructor from 1.1 is the only
  way to supply one. Replacing `Client` silently drops the auth headers.
- [ ] `services.AddQuickbase(...)` for ASP.NET Core, in a small companion package so the core
  package doesn't depend on `Microsoft.Extensions.*`.
- [ ] A query with no matches succeeds with an empty list instead of returning a `NotFound`
  failure.
- [ ] Enums for sort order and grouping, instead of the strings `"ASC"` and `"equal-values"`.
- [ ] Targets: drop net5.0 and net6.0, which are out of support and already covered by
  netstandard2.0. Add net8.0 and net10.0. Keep netstandard2.0 and net48. Move the tests and CI to
  .NET 10.
- [ ] Nullable reference annotations on the public API.
- [ ] NuGet page: package README, icon, Source Link and a symbols package.
- [ ] Migration notes in the README for every breaking change, then tag `v2.0.0`.

**Done when:** the client registers in an ASP.NET Core app with one call, a 1.x user can upgrade by
following the migration notes, and the NuGet page shows the README.

## Later

- A typed where-clause builder, such as `Where.Field(6).Contains("hello")`, that escapes quotes
  in values.
- Map records onto your own classes with attributes like `[QuickbaseField(6)]`.
- Look up field IDs by name through the fields endpoint.
- List tables and run reports.
- Retry when Quickbase rate-limits a request (HTTP 429).
- A local fake server generated from the OpenAPI definition, such as Prism, for trying the client
  by hand without a realm.

## Not planned

- **Wrapping the whole API.** Apps, users, audit logs and the rest stay out unless someone needs
  them. See "Records first".
- **Quickbase's older XML API.** The library targets the JSON API only.
