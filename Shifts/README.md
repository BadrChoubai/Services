# Shifts

This is a simple C# API demo using .NET 9.0 and Entity Framework.

## Prerequisites

Ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [SQLite](https://www.sqlite.org/download.html)

## Run the application locally

Before we can run the application locally, you should ensure that Entity Framework is installed.

```sh
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Once, that's been done we can run our initial database migration.

```sh
dotnet ef migrations add Initial 
dotnet ef database update
```

```sh
dotnet ef 
```

Initially there will be no data in the database so we'll want to seed it.

The `Program.cs` will look for an argument `seed`, and run a function if it's passed that adds a single entry to our
database for demo purposes.

```sh
dotnet run [seed]
```

### Linting

```sh
dotnet format 
```
