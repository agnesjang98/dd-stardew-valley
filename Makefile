all: packages build

packages:
	nuget install -OutputDirectory packages

build: packages
	msbuild

.PHONY: packages
