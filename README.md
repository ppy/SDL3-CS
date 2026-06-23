# SDL3-CS

C# bindings for the [SDL3](https://github.com/libsdl-org/SDL) family of libraries.

| Product                                                          | Usage                                  | Package                                                                                                                    |
|------------------------------------------------------------------|----------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| [`SDL`](https://github.com/libsdl-org/SDL/tree/main)             | `dotnet add package ppy.SDL3-CS`       | [![NuGet](https://img.shields.io/nuget/v/ppy.SDL3-CS?label=nuget)](https://www.nuget.org/packages/ppy.SDL3-CS)             |     
| [`SDL_image`](https://github.com/libsdl-org/SDL_image/tree/main) | `dotnet add package ppy.SDL3_image-CS` | [![NuGet](https://img.shields.io/nuget/v/ppy.SDL3_image-CS?label=nuget)](https://www.nuget.org/packages/ppy.SDL3_image-CS) | 
| [`SDL_ttf`](https://github.com/libsdl-org/SDL_ttf/tree/main)     | `dotnet add package ppy.SDL3_ttf-CS`   | [![NuGet](https://img.shields.io/nuget/v/ppy.SDL3_ttf-CS?label=nuget)](https://www.nuget.org/packages/ppy.SDL3_ttf-CS)     |
| [`SDL_mixer`](https://github.com/libsdl-org/SDL_mixer/tree/main) | `dotnet add package ppy.SDL3_mixer-CS` | [![NuGet](https://img.shields.io/nuget/v/ppy.SDL3_mixer-CS?label=nuget)](https://www.nuget.org/packages/ppy.SDL3_mixer-CS) |

Contributions to keep the bindings up-to-date with upstream changes are welcome. If you have improvements or updates, feel free to submit a pull request.

## Platform support

| Product         | `win-x64` | `win-x86` | `win-arm64` | `osx-arm64` | `osx-x64` | `linux-x64` | `linux-x86` | `linux-arm64` | `linux-arm` | `ios`   | `android` |
|-----------------|-----------|-----------|-------------|-------------|-----------|-------------|-------------|---------------|-------------|---------|-----------|
| `SDL3-CS`       | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     | &check; | &check;   |
| `SDL3_image-CS` | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     | &check; | &check;   |
| `SDL3_ttf-CS`   | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     | &check; | &check;   |
| `SDL3_mixer-CS` | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     | &check; | API 24+   |

## How to update SDL

1. Ensure all submodules are correctly initialised:
   ```sh
   git submodule update --init --recursive
   ```
2. Check out submodules at new desired upstream commits.
3. Regenerate C# bindings via:
   ```sh
   docker build -t 'sdl-gen' .
   docker run --rm -v .:/app -w /app -it sdl-gen
   ```
4. Run the "Build Native" (`.github/workflows/build.yml`) workflow to generate platform binaries.

## License

This code is released under [MIT](LICENCE).
