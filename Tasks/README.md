# Tasks API

This is a simple API built with **C#** and **ASP.NET Core**. It provides a development and production setup using the **.NET CLI** for building, running, testing, and linting.

## Project Structure

```
.
├── appsettings.Development.json  // Development-specific configuration
├── appsettings.json              // Base configuration
├── Dockerfile                     // Container build instructions (optional)
├── Program.cs                     // Entry point of the application
├── Properties/
│   └── launchSettings.json       // Local development launch profiles
├── Tasks.csproj                    // Project file (dependencies, SDK, etc.)
├── Tasks.http                      // Example HTTP requests for testing
├── README.md                       // This file
└── bin/ & obj/                     // Build output and intermediate files (generated)
```

## Configuration

- The app uses `appsettings.json` for base configuration.
- `appsettings.Development.json` is used when running in development mode.
- `launchSettings.json` defines profiles for running via Visual Studio, JetBrains Rider, or `dotnet run`.

## Prerequisites

Ensure you have the following installed:

- [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [.NET 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- (Optional) [Docker](https://www.docker.com/) if you want to containerize the app.

---

## Development Mode (with Hot Reloading)

To start the application in development mode (with automatic recompilation and restart on file changes):

```sh
dotnet watch run
```

## Production Mode

### Build the Project

To compile the application for production:

```sh
dotnet publish -c Release -o ./publish
```

This will generate the production-ready files into the `publish/` folder.

### Run the Compiled Application

To run the published application:

```sh
dotnet ./publish/Tasks.dll
```

## Linting / Formatting

To check and automatically fix code style issues using `dotnet format`:

```sh
dotnet format
```

## Testing

If you have unit tests (in a separate test project), you can run:

```sh
dotnet test
```


## Cleaning

To remove compiled files and artifacts:

```sh
dotnet clean
```

You can also manually remove `bin/` and `obj/` if needed.

## Docker

To build a Docker image (if your Dockerfile supports this):

```sh
docker build -t tasks-api .
```
