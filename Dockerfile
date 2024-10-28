FROM ubuntu:24.04

RUN apt-get update && \
    apt-get install -y dotnet-sdk-8.0 python3 git build-essential

WORKDIR /tmp
COPY .config/dotnet-tools.json .config/dotnet-tools.json
COPY SDL3-CS/SDL3-CS.csproj SDL3-CS/SDL3-CS.csproj
RUN dotnet tool restore && \
    dotnet restore --ucr SDL3-CS/SDL3-CS.csproj

WORKDIR /
RUN echo '#!/bin/bash' >> entrypoint.sh && \
    echo 'export LD_LIBRARY_PATH="$(echo ~/.nuget/packages/libclang.runtime.*/*/runtimes/*/native):$(echo ~/.nuget/packages/libclangsharp.runtime.*/*/runtimes/*/native):${LD_LIBRARY_PATH:-}"' >> entrypoint.sh && \
    echo 'export CPLUS_INCLUDE_PATH="$(echo /usr/lib/gcc/*/*/include):/usr/include:${CPLUS_INCLUDE_PATH:-}"' >> entrypoint.sh && \
    echo 'python3 SDL3-CS/generate_bindings.py "$@"' >> entrypoint.sh && \
    chmod +x entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]