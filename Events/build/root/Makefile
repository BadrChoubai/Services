include build/root/config.mk

dist: # @HELP create and push docker image to registry
dist: docker-image docker-push
	@echo 'docker image created, tagged, and pushed to registry:'
	@echo '$(REGISTRY)'
	@echo ''

docker-image: # @HELP create docker image
docker-image:
	@echo 'building docker image'
	$(DOCKER_CMD) build -t events-service .

docker-push: # @HELP tag and push docker image to registry
docker-push:
	@echo 'tagging and push docker image to registry:'
	@echo '$(REGISTRY)'
	$(DOCKER_CMD) tag events-service $(REGISTRY)
	$(DOCKER_CMD) push $(REGISTRY)

docker-run: # @HELP run the docker container using the image in the registry
docker-run:
	$(DOCKER_CMD) run -p 8084:8080 $(REGISTRY)

clean: # @HELP clean up build artifacts
clean: dist-clean

dist-clean:
	rm -rf dist

BIN ?= api-template
SHELL := /usr/bin/env bash -o errexit -o pipefail -o nounset

.DEFAULT_GOAL = dist
.PHONY: clean dist-clean