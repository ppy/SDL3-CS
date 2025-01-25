#!/bin/bash

set -e

pushd "$(dirname "$0")"

# Check if environment variables are defined
if [[ -z $NAME || -z $RUNNER_OS || -z $FLAGS ]]; then
    echo "One or more required environment variables are not defined."
    exit 1
fi

SUDO=$(which sudo || exit 0)

export DEBIAN_FRONTEND=noninteractive

if [[ $RUNNER_OS == 'Linux' ]]; then
# Setup Linux dependencies
    if [[ $TARGET_APT_ARCH == :i386 ]]; then
        $SUDO dpkg --add-architecture i386
    fi

    $SUDO apt-get update -y -qq

    if [[ $TARGET_APT_ARCH == :i386 ]]; then
        # Workaround GitHub's ubuntu-20.04 image issue <https://github.com/actions/virtual-environments/issues/4589>
        $SUDO apt-get install -y --allow-downgrades libpcre2-8-0=10.34-7
    fi

    if [[ $NAME != 'linux-x86' && $NAME != 'linux-x64' ]]; then
        GCC="gcc"
        GPP="g++"
    else
        GCC="gcc-multilib"
        GPP="g++-multilib"
    fi

    $SUDO apt-get install -y \
        $GCC \
        $GPP \
        git \
        cmake \
        ninja-build \
        wayland-scanner++ \
        wayland-protocols \
        meson \
        pkg-config$TARGET_APT_ARCH \
        libasound2-dev$TARGET_APT_ARCH \
        libdbus-1-dev$TARGET_APT_ARCH \
        libegl1-mesa-dev$TARGET_APT_ARCH \
        libgl1-mesa-dev$TARGET_APT_ARCH \
        libgles2-mesa-dev$TARGET_APT_ARCH \
        libglu1-mesa-dev$TARGET_APT_ARCH \
        libgtk-3-dev$TARGET_APT_ARCH \
        libibus-1.0-dev$TARGET_APT_ARCH \
        libpango1.0-dev$TARGET_APT_ARCH \
        libpulse-dev$TARGET_APT_ARCH \
        libsndio-dev$TARGET_APT_ARCH \
        libudev-dev$TARGET_APT_ARCH \
        libwayland-dev$TARGET_APT_ARCH \
        libx11-dev$TARGET_APT_ARCH \
        libxcursor-dev$TARGET_APT_ARCH \
        libxext-dev$TARGET_APT_ARCH \
        libxi-dev$TARGET_APT_ARCH \
        libxinerama-dev$TARGET_APT_ARCH \
        libxkbcommon-dev$TARGET_APT_ARCH \
        libxrandr-dev$TARGET_APT_ARCH \
        libxss-dev$TARGET_APT_ARCH \
        libxt-dev$TARGET_APT_ARCH \
        libxv-dev$TARGET_APT_ARCH \
        libxxf86vm-dev$TARGET_APT_ARCH \
        libdrm-dev$TARGET_APT_ARCH \
        libgbm-dev$TARGET_APT_ARCH \
        libpulse-dev$TARGET_APT_ARCH

    if [[ $TARGET_APT_ARCH != :i386 ]]; then
        # Build libdecor.
        # This is required so that window decorations can work on wayland.
        # The support will only be enabled in SDL, but we're not shipping the libdecor binaries
        # because making them work from a c# app as everything else does (via runtimes) is too difficult.
        # Also skip i386 because attempting to support this for i386 is a pain.
        # Special shoutouts to gnome for refusing to support server-side decorations.
        git clone https://gitlab.freedesktop.org/libdecor/libdecor.git
        cd libdecor
        git checkout 0.2.2
        meson build --buildtype release
        $SUDO meson install -C build
        cd ..
    fi
fi

# Build SDL
pushd SDL
git reset --hard HEAD || echo "Failed to clean up the repository"

if [[ $RUNNER_OS == 'Windows' ]]; then
    echo "Patching SDL to not include gameinput.h"
    sed -i 's/#include <gameinput.h>/#_include <gameinput.h>/g' CMakeLists.txt
fi

cmake -B build $FLAGS -DCMAKE_BUILD_TYPE=$BUILD_TYPE -DSDL_SHARED_ENABLED_BY_DEFAULT=ON -DSDL_STATIC_ENABLED_BY_DEFAULT=ON
cmake --build build/ --config Release
$SUDO cmake --install build/ --prefix install_output --config Release
popd

# Move build lib into correct folders
if [[ $RUNNER_OS == 'Windows' ]]; then
    cp SDL/install_output/bin/SDL3.dll ../native/$NAME/SDL3.dll
elif [[ $RUNNER_OS == 'Linux' ]]; then
    cp SDL/install_output/lib/libSDL3.so ../native/$NAME/libSDL3.so
elif [[ $RUNNER_OS == 'macOS' ]]; then
    cp SDL/install_output/lib/libSDL3.dylib ../native/$NAME/libSDL3.dylib
fi

popd
