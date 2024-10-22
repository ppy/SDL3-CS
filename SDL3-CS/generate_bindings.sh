#!/bin/bash

set -eu
pushd "$(dirname "$0")/../" >/dev/null

dotnet tool restore
dotnet restore --ucr SDL3-CS/SDL3-CS.csproj

export LD_LIBRARY_PATH="$(echo ~/.nuget/packages/libclang.runtime.*/*/runtimes/*/native):$(echo ~/.nuget/packages/libclangsharp.runtime.*/*/runtimes/*/native):${LD_LIBRARY_PATH:-}"
export CPLUS_INCLUDE_PATH="$(echo /usr/lib/gcc/*/*/include):/usr/include:${CPLUS_INCLUDE_PATH:-}"

python3 SDL3-CS/generate_bindings.py "$@"

popd >/dev/null
