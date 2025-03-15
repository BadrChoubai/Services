# Tasks

This is a Python API service used to assign tasks to employees of a warehouse facility.

## Prerequisites

- [Docker](https://www.docker.com/) (For containerized build and development)
- [uv](https://docs.astral.sh/uv/) for Python package and environment management

### Installing Dependencies and Attaching to `venv` Locally

```shell
uv sync
source .venv/bin/activate
# or
make activate
```

## Running the Application

Setting up and running the application may be done using a task
contained in `Makefile` or with `docker compose watch`

### Local Development

To run the application locally use `make run` which will run our application locally,
you may also use docker compose in watch mode. 

```shell
docker compose watch
```

## Testing the Application

No tests have been added yet.

```shell
make test
```

## Linting

Linting and formatting is achieved using [`ruff`](https://docs.astral.sh/ruff/) and 
is run on all files located in the `app/` and `scripts/` directories.

- `make format` - runs `format.sh`
- `make lint` - runs `lint.sh`

