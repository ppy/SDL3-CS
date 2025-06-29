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
| `SDL3_image-CS` | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     |         |           |
| `SDL3_ttf-CS`   | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     |         |           |
| `SDL3_mixer-CS` | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     |         |           |