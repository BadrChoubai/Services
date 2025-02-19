include build/root/config.mk
include .env

BIN ?= api
BIN_OUT := .dist/$(OS)_$(ARCH)

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

lint: # @HELP runs golangci-lint linters
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

run: # @HELP select and run a single application
run:
	@echo "$(BINS)"
	@read -p "Select an application to run: " selection; \
	if echo "$(BINS)" | grep -wq $$selection; then \
	    $(PWD)/build/scripts/run-locally.sh $$selection \
	else \
	  printf  "application: \"%s\", does not exist.\n" $$selection; \
	fi

SHELL := /usr/bin/env bash -o errexit -o pipefail
.DEFAULT_GOAL = build
.PHONY: all clean
