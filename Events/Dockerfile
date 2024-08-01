# Stage 1: Build
FROM golang:1.22 AS build
WORKDIR /build

# Copy go mod and sum files
COPY go.mod go.sum ./
RUN go mod download

# Copy the source code
COPY . .

# Build the Go application
ENV CGO_ENABLED=0
RUN go build -o /dist/main main.go

# Stage 2: Final
FROM alpine:latest AS final
WORKDIR /app

# Copy the built binary from the build stage
COPY --from=build /build/config.json ./config.json
COPY --from=build /dist .

# Expose the port the application will run on
EXPOSE 8080
# Optionally expose other ports if needed
# EXPOSE 443

# Define the command to run the application
ENTRYPOINT ["./main"]