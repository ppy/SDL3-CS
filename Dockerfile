FROM ubuntu:24.04

RUN apt-get update && apt-get install -y \
    dotnet-sdk-8.0 \
    python3 \
    git \
    build-essential

RUN ln -s /usr/bin/python3 /usr/bin/python

SHELL ["/bin/bash", "-c"]