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
docker build -t $USER-user-service .
```

### Run the Container Image

```bash
docker run $USER-user-service 
```

## Available Routes

| Method | Path      |             URL              |
|:------:|:----------|:----------------------------:|
|  GET   | `/health` | http://127.0.0.1:8080/health |


### Test 

```bash
curl -X GET --location "http://localhost:8080/health"
```
