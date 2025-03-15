# Employees

This is a Go-based API project with a Makefile-driven build and deployment process. The project includes support for
building, linting, containerizing, and running the application.

## Prerequisites

Ensure you have the following installed:

- [Go](https://go.dev/) (Latest stable version recommended)
- [Docker](https://www.docker.com/) (For containerized builds)
- [golangci-lint](https://golangci-lint.run/) (For linting)
- [Make](https://www.gnu.org/software/make/) (For build automation)

## Running the Application

### Build the Project

To compile the Go application:

```sh
make build
```

This builds the binary and places it into `.dist/$(OS)_$(ARCH)/api`.

### Run the Application

To run the application locally:

```sh
make run
```

### Clean Build Artifacts

To remove build artifacts:

```sh
make clean
```

### Linting

To check code quality using `golangci-lint`:

```sh
make lint
```

If linting fails, you will be prompted to retry with the `--fix` flag.

## Database Migrations

Database migrations are driven by tasks in `/migrations/Makefile`.

[Documentation](./migrations/README.md)

## Docker

To build and manage Docker images:

```sh
make docker
```

This will generate a Dockerfile and build the corresponding image.

## Project Structure

```
go-api-project/
├── cmd/              # Application entry points
├── build/            # Build scripts and configurations
├── .dist/            # Compiled binaries
├── Makefile          # Build automation
├── Dockerfile.in     # Docker template
└── .env              # Environment variables
```
