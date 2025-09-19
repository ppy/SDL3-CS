#!/bin/bash

set -e

pushd "$(dirname "$0")"

# Check if environment variables are defined
if [[ -z $NAME || -z $RUNNER_OS || -z $FLAGS || -z $BUILD_TYPE ]]; then
    echo "One or more required environment variables are not defined."
    exit 1
fi

if [[ $RUNNER_OS == 'Windows' ]]; then
    SUDO=""
else
    SUDO=$(which sudo || exit 0)
fi

if [[ -n $ANDROID_ABI ]]; then
    BUILD_PLATFORM="Android"
else
    BUILD_PLATFORM="$RUNNER_OS"
fi

export DEBIAN_FRONTEND=noninteractive

if [[ $BUILD_PLATFORM != 'Android' ]]; then
    NATIVE_PATH="$NAME"

    if [[ $BUILD_PLATFORM == 'Linux' ]]; then
        # Setup Linux dependencies
        if [[ $TARGET_APT_ARCH == :i386 ]]; then
            $SUDO dpkg --add-architecture i386
        fi

        $SUDO apt-get update -y -qq

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
            libpulse-dev$TARGET_APT_ARCH \
            libpipewire-0.3-dev$TARGET_APT_ARCH \
            libdecor-0-dev$TARGET_APT_ARCH
    fi
else
    if [[ -z $ANDROID_HOME || -z $NDK_VER || -z $ANDROID_ABI ]]; then
        echo "One or more required environment variables are not defined."
        exit 1
    fi

    NATIVE_PATH="android/$ANDROID_ABI"

    export ANDROID_NDK_HOME="$ANDROID_HOME/ndk/$NDK_VER"
    export FLAGS="$FLAGS -DCMAKE_TOOLCHAIN_FILE=$ANDROID_NDK_HOME/build/cmake/android.toolchain.cmake \
                         -DANDROID_HOME=$ANDROID_HOME \
                         -DANDROID_PLATFORM=21 \
                         -DANDROID_ABI=$ANDROID_ABI \
                         -DCMAKE_POSITION_INDEPENDENT_CODE=ON \
                         -DCMAKE_FIND_ROOT_PATH_MODE_PACKAGE=BOTH \
                         -DCMAKE_INSTALL_INCLUDEDIR=include \
                         -DCMAKE_INSTALL_LIBDIR=lib \
                         -DCMAKE_INSTALL_DATAROOTDIR=share \
                         -DSDL_ANDROID_JAR=OFF"

    $SUDO apt-get install -y \
            git \
            cmake \
            ninja-build \
            meson
fi

if [[ $RUNNER_OS == 'Linux' ]]; then
    git config --global --add safe.directory $PWD/SDL
    git config --global --add safe.directory $PWD/SDL_image
    git config --global --add safe.directory $PWD/SDL_ttf
    git config --global --add safe.directory $PWD/SDL_mixer
fi

CMAKE_INSTALL_PREFIX="$PWD/install_output"
rm -rf $CMAKE_INSTALL_PREFIX

if [[ $BUILD_PLATFORM == 'Android' ]]; then
    OUTPUT_LIB="lib/libSDL3variant.so"
elif [[ $BUILD_PLATFORM == 'Windows' ]]; then
    OUTPUT_LIB="bin/SDL3variant.dll"
elif [[ $BUILD_PLATFORM == 'Linux' ]]; then
    OUTPUT_LIB="lib/libSDL3variant.so"
elif [[ $BUILD_PLATFORM == 'macOS' ]]; then
    OUTPUT_LIB="lib/libSDL3variant.dylib"
fi

# Use the correct CMAKE_PREFIX_PATH for SDL_image and SDL_ttf, probably due differences in Cmake versions.
if [[ $BUILD_PLATFORM == 'Android' ]]; then
    CMAKE_PREFIX_PATH="$CMAKE_INSTALL_PREFIX"
elif [[ $BUILD_PLATFORM == 'Windows' ]]; then
    CMAKE_PREFIX_PATH="$CMAKE_INSTALL_PREFIX/cmake/"
elif [[ $BUILD_PLATFORM == 'Linux' ]]; then
    CMAKE_PREFIX_PATH="$CMAKE_INSTALL_PREFIX/lib/cmake/"
elif [[ $BUILD_PLATFORM == 'macOS' ]]; then
    CMAKE_PREFIX_PATH="$CMAKE_INSTALL_PREFIX/lib/cmake/"
fi

run_cmake() {
    LIB_NAME=$1
    LIB_OUTPUT=$2

    pushd $LIB_NAME

    git reset --hard HEAD || echo "Failed to clean up the repository"

    if [[ $BUILD_PLATFORM == 'Windows' && $LIB_NAME == 'SDL' ]]; then
        echo "Patching SDL to not include gameinput.h"
        sed -i 's/#include <gameinput.h>/#_include <gameinput.h>/g' CMakeLists.txt
    fi

    # Change the minumum Android API level for SDL_mixer to API 24 as opusfile and libflac fail to build on lower versions.
    if [[ $BUILD_PLATFORM == 'Android' && $LIB_NAME == 'SDL_mixer' ]]; then
        export FLAGS="${FLAGS/-DANDROID_PLATFORM=21/-DANDROID_PLATFORM=24}"
    fi

    rm -rf build
    cmake -B build $FLAGS -DCMAKE_BUILD_TYPE=$BUILD_TYPE -DSDL_SHARED=ON -DSDL_STATIC=OFF "${@:3}"
    cmake --build build/ --config $BUILD_TYPE --verbose
    cmake --install build/ --prefix $CMAKE_INSTALL_PREFIX --config $BUILD_TYPE

    # Move build lib into correct folders
    cp $CMAKE_INSTALL_PREFIX/$LIB_OUTPUT ../../native/$NATIVE_PATH

    popd
}

run_cmake SDL ${OUTPUT_LIB/variant/}

run_cmake SDL_ttf ${OUTPUT_LIB/variant/_ttf} -DCMAKE_PREFIX_PATH=$CMAKE_PREFIX_PATH -DCMAKE_POLICY_VERSION_MINIMUM=3.5 -DSDLTTF_VENDORED=ON

# -DSDLIMAGE_AVIF=OFF is used because windows requires special setup to build avif support (nasm)
# TODO: Add support for avif on windows (VisualC script uses dynamic imports)
run_cmake SDL_image ${OUTPUT_LIB/variant/_image} -DCMAKE_PREFIX_PATH=$CMAKE_PREFIX_PATH -DSDLIMAGE_AVIF=OFF -DSDLIMAGE_DEPS_SHARED=OFF -DSDLIMAGE_VENDORED=ON

# -DSDLMIXER_MP3_MPG123=OFF is used because upstream build is broken. Fallback to dr_mp3.
# See: https://github.com/libsdl-org/SDL_mixer/pull/744#issuecomment-3180682130
# Fixing using the proposed solution causes more issues.
run_cmake SDL_mixer ${OUTPUT_LIB/variant/_mixer} -DCMAKE_PREFIX_PATH=$CMAKE_PREFIX_PATH -DSDLMIXER_MP3_MPG123=OFF -DSDLMIXER_DEPS_SHARED=OFF -DSDLMIXER_VENDORED=ON

popd
