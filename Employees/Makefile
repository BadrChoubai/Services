include .env
include migrations/Makefile

all: # @HELP builds the docker image for our application
all:
	@echo "Building ${DOCKER_IMAGE}"
	docker build \
	--progress=plain \
	-t $(REGISTRY)/$(DOCKER_IMAGE) \
	-f Dockerfile \
	.

run: # @HELP run the application locally, watching for changes locally (requires air: https://github.com/air-verse/air)
run:
	air

test: # @HELP run any tests
test:
	go test ./...

lint: # @HELP lints project files and scripts
lint:
	sh ./build/scripts/lint.sh

activate: # @HELP install dependencies and activate environment
activate:
	uv sync
	source .venv/bin/activate

clean: # @HELP cleans temporary files and build artifacts created during development
clean:
	rm -rf .dist tmp

help-vars:
	@echo "VARIABLES:"
	@grep -hE '^# *[A-Z0-9_]+: ' $(MAKEFILE_LIST) \
		| sed -E 's/^# *([A-Z0-9_]+): (.*)/  \1: \2/'

help-targets:
	@printf "\nTARGETS\n"
	@grep -hE '^.*: *# *@HELP' $(MAKEFILE_LIST)     \
	    | awk '                                   \
	        BEGIN {FS = ": *# *@HELP"};           \
	        { printf "  %-30s %s\n", $$1, $$2 };  \
	    '

help: # @HELP prints help messages
help: help-vars help-targets

# REGISTRY: Where should Docker images be pushed once they're built?
REGISTRY ?= localhost:5000

SHELL := /usr/bin/env bash -o errexit -o pipefail -o nounset
.DEFAULT_GOAL = all
.PHONY: all activate clean format help lint run test
