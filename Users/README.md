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

### Build the Container Image

```bash
docker build -t user-service -f Dockerfile
```

### Run the Container Image

```bash
docker run user-service 
```

## Available Routes

| Method | Path      |             URL              |
|:------:|:----------|:----------------------------:|
|  GET   | `/health` | http://localhost:8080/health |


### Test 

```bash
curl -X GET --location "http://localhost:8080/health"
```
