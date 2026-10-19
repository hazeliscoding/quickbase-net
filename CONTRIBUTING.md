# Contributing

Issues and pull requests are welcome. Issues labeled
[`good first issue`](https://github.com/hazeliscoding/quickbase-net/labels/good%20first%20issue) are
small and self-contained. After those, the next unchecked item in [ROADMAP.md](ROADMAP.md) is what's
being built.

Comment on an issue before you start, so two people don't take the same one.

## Setup

You need the .NET 8 SDK or later. Then:

```sh
dotnet build
dotnet test
```

All five targets build on Windows, macOS and Linux, net48 included. The tests run against a mocked
HTTP handler, so you don't need a Quickbase account or token.

## Pull requests

- One change per pull request, linked to its issue.
- Tests for new behavior.
- Commit messages follow [Conventional Commits](https://www.conventionalcommits.org/), such as
  `feat(query): add Skip and Top`.
- 1.x stays compatible: add methods and overloads, but don't change an existing signature or result.
  Those changes wait for 2.0 (see [ROADMAP.md](ROADMAP.md)).
- CI builds and tests every pull request, and it has to pass.
