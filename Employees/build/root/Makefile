include build/root/config.mk
include .env

BIN ?= api
BIN_OUT := .dist/$(OS)_$(ARCH)

run: # @HELP select and run a single application
run:
	$(PWD)/build/scripts/run-locally.sh $(BIN)

build: # @HELP for each BIN, build binary and place into /bin/$(OS)_$(ARCH)
build: lint $(BIN)

# Build each binary using Docker
$(BIN): $(BIN_OUT)
	    go build -o .dist/$(OS)_$(ARCH)/"$@" ./cmd/"$@"

$(BIN_OUT):
	mkdir -p $@

clean: # @HELP clean artifacts from build
clean:
	rm -rf .dist .Dockerfile-* .Image-*

DOCKERFILE = .Dockerfile-$(BIN)_$(OS)__$(ARCH)
DOCKERIMAGE = .Image-$(BIN)-$(TAG)

docker: # @HELP run docker lifecycle tasks
docker: $(DOCKERFILE) $(DOCKERIMAGE)

.Dockerfile-%_$(OS)__$(ARCH):
	@echo "$@"
	sed \
	-e 's|{ARG_BIN}|$(BIN)$(BIN_EXTENSION)|g'  \
	-e 's|{ARG_ARCH}|$(ARCH)|g'                \
	-e 's|{ARG_OS}|$(OS)|g'                    \
	-e 's|{ARG_FROM}|$(CONTAINER_IMAGE)|g'     \
	Dockerfile.in > $@

.Image-%-$(TAG):
	docker build \
		--progress=plain \
		-t $(REGISTRY)/$(BIN):$(TAG) \
		-f .Dockerfile-$(BIN)_$(OS)__$(ARCH) \
		.
	touch $@

lint: lint-go lint-docs

lint-docs: # @HELP runs golangci-lint linters
	npx prettier --write "**/*.{md,json,yaml}"


lint-go: # @HELP runs golangci-lint linters
	@if golangci-lint run; then \
		echo "golangci-lint ran successfully"; \
	else \
		echo "golangci-lint failed to lint project files"; \
		read -p "Would you like to try running it again with --fix (y/n)? " selection; \
		if [ "$$selection" = "y" ]; then \
			echo "Running golangci-lint with --fix..."; \
			golangci-lint run --fix; \
			if [ $$? -eq 0 ]; then \
				echo "golangci-lint ran successfully with --fix"; \
			else \
				echo "golangci-lint still failed to lint project files."; \
			fi; \
		else \
			echo "Skipping fix attempt. Please review the linting errors."; \
		fi; \
	fi

####################################################################################################
# Database Tasks
# https://github.com/golang-migrate/migrate/tree/master/cmd/migrate#migrate-cli
####################################################################################################

db/info: # @HELP prints value of DB_CONNECTION_STRING
db/info:
	$(MAKE) -C ./migrations db/info

db/connect: # @HELP connect to locally running database
db/connect:
	$(MAKE) -C ./migrations db/connect

db/migrations/up: # @HELP run migrations to create database tables
db/migrations/up:
	$(MAKE) -C ./migrations db/migrations/up

db/migrations/down: # @HELP run migrations to teardown database tables
db/migrations/down:
	$(MAKE) -C ./migrations db/migrations/down

db/migrations/refresh: # @HELP refresh database tables
db/migrations/refresh: db/migrations/down db/migrations/up

SHELL := /usr/bin/env bash -o errexit -o pipefail
.DEFAULT_GOAL = build
.PHONY: all clean
