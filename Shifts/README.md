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
dotnet ef migrations add <name_of_file>
dotnet ef database update
```

Initially there will be no data in the database so we'll want to seed it. 

Running `dotnet run` for the first time with the argument `seed` will run a utility script
called by `Program.cs`.

```sh
dotnet run [seed]
```

### Linting

```sh
dotnet format 
```
