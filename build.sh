#!/bin/bash

# Check if environment variables are defined
if [[ -z $NAME || -z $RUNNER_OS || -z $FLAGS ]]; then
    echo "One or more required environment variables are not defined."
    exit 1
fi

SUDO=$(which sudo)

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
        cmake \
        ninja-build \
        wayland-scanner++ \
        wayland-protocols \
        pkg-config$TARGET_APT_ARCH \
        libasound2-dev$TARGET_APT_ARCH \
        libdbus-1-dev$TARGET_APT_ARCH \
        libegl1-mesa-dev$TARGET_APT_ARCH \
        libgl1-mesa-dev$TARGET_APT_ARCH \
        libgles2-mesa-dev$TARGET_APT_ARCH \
        libglu1-mesa-dev$TARGET_APT_ARCH \
        libibus-1.0-dev$TARGET_APT_ARCH \
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
fi

# Configure CMake
if [[ $NAME == 'linux-x86' ]]; then
    FLAGS="$FLAGS -DCMAKE_C_FLAGS=-m32 -DCMAKE_CXX_FLAGS=-m32"
elif [[ $NAME == 'linux-x64' ]]; then
    FLAGS="$FLAGS -DCMAKE_C_FLAGS=-m64 -DCMAKE_CXX_FLAGS=-m64"
fi

cmake -B build $FLAGS -DCMAKE_BUILD_TYPE=$BUILD_TYPE -DSDL_SHARED_ENABLED_BY_DEFAULT=ON -DSDL_STATIC_ENABLED_BY_DEFAULT=ON

# Build
cmake --build build/ --config Release

if [[ $RUNNER_OS == 'Windows' ]]; then
    # Install (Windows)
    cmake --install build/ --prefix install_output --config Release
else
    # Install
    $SUDO cmake --install build/ --prefix install_output --config Release
fi

mkdir -p SDL3-CS/native/$NAME

if [[ $RUNNER_OS == 'Windows' ]]; then
    # Prepare release (Windows)
    cp install_output/bin/SDL3.dll SDL3-CS/native/$NAME/SDL3.dll
elif [[ $RUNNER_OS == 'Linux' ]]; then
    # Prepare release (Linux)
    cp install_output/lib/libSDL3.so SDL3-CS/native/$NAME/libSDL3.so
elif [[ $RUNNER_OS == 'macOS' ]]; then
    # Prepare release (macOS)
    cp install_output/lib/libSDL3.dylib SDL3-CS/native/$NAME/libSDL3.dylib
fi