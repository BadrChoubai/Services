# Users

## Developing Locally

### Restore Packages

```bash
dotnet restore
```

### Run the Service

```bash
dotnet run
```

## Docker

> Before you can start to build the container image locally, it is recommended to setup a Docker registry locally. Instructions
can be found in the [Services-infrastructure](https://github.com/BadrChoubai/Services-infrastructure) Repository.

### Build and Push the Container Image to your Local Registry

```bash
docker build -t user-service .
docker tag user-service localhost:5000/$USER/user-service
docker push localhost:5000/$USER/user-service
```

### Run the Container Image

```bash
docker run -p 8080:80 localhost:5000/$USER/user-service
```

## Available Routes

| Method | Path      |             URL              |
|:------:|:----------|:----------------------------:|
|  GET   | `/health` | http://127.0.0.1:8080/health |


### Test 

```bash
curl -X GET --location "http://localhost:8080/health"
```
