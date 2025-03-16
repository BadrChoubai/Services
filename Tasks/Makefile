include .env

all: # @HELP builds the docker image for our application
all: lint format
	@echo "Building ${DOCKER_IMAGE}"
	docker build \
	--progress=plain \
	-t $(REGISTRY)/$(DOCKER_IMAGE) \
	-f Dockerfile \
	.

run: # @HELP run the application locally and watch for changes
run:
	fastapi run --reload app/main.py

test: # @HELP run any tests
test:
	@echo "No tests to run"

lint: # @HELP lints project files and scripts
lint:
	sh ./scripts/lint.sh

format: # @HELP formats project files and scripts
	sh ./scripts/format.sh

activate: # @HELP install dependencies and activate environment
activate:
	uv sync
	source .venv/bin/activate

clean: # @HELP cleans temporary files and build artifacts
clean:
	rm -rf __pycache__
	rm -rf $(VENV)

help-vars:
		@echo "VARIABLES:"
		@grep -hE '^# *[A-Z0-9_]+: ' $(MAKEFILE_LIST) \
			| sed -E 's/^# *([A-Z0-9_]+): (.*)/  \1: \2/'
		@echo

help: # @HELP prints this help message
help: help-vars
	@echo "TARGETS"
	@grep -hE '^.*: *# *@HELP' $(MAKEFILE_LIST)     \
	    | awk '                                   \
	        BEGIN {FS = ": *# *@HELP"};           \
	        { printf "  %-30s %s\n", $$1, $$2 };  \
	    '

# REGISTRY: Where should Docker images be pushed once they're built?
REGISTRY ?= localhost:5000

SHELL := /usr/bin/env bash -o errexit -o pipefail -o nounset
.DEFAULT_GOAL = all
.PHONY: all activate clean format help lint test
