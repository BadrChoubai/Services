# Locations 

## Developing Locally

### Install Packages

```bash
npm install
```

### Run the Service

```bash
npm run inspect
```

## Docker

> Before you can start to build the container image locally, it is recommended to setup a Docker registry locally. Instructions
can be found in the [Services-infrastructure](https://github.com/BadrChoubai/Services-infrastructure) Repository.

### Build and Push the Container Image to your Local Registry

```bash
docker build -t location-service .
docker tag location-service localhost:5000/$USER/location-service
docker push localhost:5000/$USER/location-service
```

### Run the Container Image

```bash
docker run -p 8082:80 localhost:5000/$USER/location-service
```

## Available Routes

| Method | Path      |               URL               |
|:------:|:----------|:-------------------------------:|
|  GET   | `/health` |  http://127.0.0.1:8082/health   |


### Test

```bash
curl -X GET --location "http://localhost:8082/health"
```
