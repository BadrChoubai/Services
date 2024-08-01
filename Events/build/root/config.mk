PLATFORMS ?= linux/amd64

# Used internally.  Users should pass GOOS and/or GOARCH.
OS := $(if $(GOOS),$(GOOS),$(shell GOTOOLCHAIN=local go env GOOS))
ARCH := $(if $(GOARCH),$(GOARCH),$(shell GOTOOLCHAIN=local go env GOARCH))

GO_VERSION := 1.22
CONTAINER_IMAGE := golang:$(GO_VERSION)-alpine
#VERSION ?= $(shell git describe --tags --always --dirty)
VERSION ?= 1.0.0
IMAGE_TAG := $(VERSION)__$(OS)_$(ARCH)
# Credentials to access the registry.
REGISTRY_USERNAME ?= buddy
#REGISTRY_PASSWORD ?= $$(registry_cli auth print-access-token)
# Where to push the docker images.
REGISTRY ?= localhost:5000/$(REGISTRY_USERNAME)/events-service
DOCKER_CMD := docker

# It's necessary to set this because some environments don't link sh -> bash.
SHELL := /usr/bin/env bash -o errexit -o pipefail -o nounset

help:
	echo "TARGETS:"
	grep -E '^.*: *# *@HELP' $(MAKEFILE_LIST)     \
	    | awk '                                   \
	        BEGIN {FS = ": *# *@HELP"};           \
	        { printf "  %-30s %s\n", $$1, $$2 };  \
	    '

.SILENT: help
.PHONY: help
