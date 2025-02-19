include build/root/config.mk
include .env

BINS ?= api
BINS_OUT := .dist/$(OS)_$(ARCH)

IMAGE_DOTFILES = $(foreach bin,$(BINS),.image-$(bin)-$(TAG))

build: # @HELP for each BIN, build binary and place into /bin/$(OS)_$(ARCH)
build: lint $(BINS)

# Build each binary using Docker
$(BINS): $(BINS_OUT)
	    go build -o .dist/$(OS)_$(ARCH)/"$@" ./cmd/"$@"

$(BINS_OUT):
	mkdir -p $@

clean: # @HELP clean artifacts from build
clean:
	rm -rf .dist

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
