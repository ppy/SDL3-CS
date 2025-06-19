# SDL3-CS

C# bindings for the [SDL3](https://github.com/libsdl-org/SDL) family of libraries.

| Product                                                          | Usage                                  | Package                                                                                                                                
|------------------------------------------------------------------|----------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------
| [`SDL`](https://github.com/libsdl-org/SDL/tree/main)             | `dotnet add package ppy.SDL3-CS`       | [![NuGet](https://img.shields.io/nuget/v/ppy.SDL3-CS?label=ppy.SDL3-CS)](https://www.nuget.org/packages/ppy.SDL3-CS)                   
| [`SDL_image`](https://github.com/libsdl-org/SDL_image/tree/main) | `dotnet add package ppy.SDL3_image-CS` | [![NuGet](https://img.shields.io/nuget/v/ppy.SDL3_image-CS?label=ppy.SDL3_image-CS)](https://www.nuget.org/packages/ppy.SDL3_image-CS) 
| [`SDL_ttf`](https://github.com/libsdl-org/SDL_ttf/tree/main)     | `dotnet add package ppy.SDL3_ttf-CS`   | [![NuGet](https://img.shields.io/nuget/v/ppy.SDL3_ttf-CS?label=ppy.SDL3_ttf-CS)](https://www.nuget.org/packages/ppy.SDL3_ttf-CS)       

Contributions to keep the bindings up-to-date with upstream changes are welcome. If you have improvements or updates, feel free to submit a pull request.

## Platform support

| Product         | `win-x64` | `win-x86` | `win-arm64` | `osx-arm64` | `osx-x64` | `linux-x64` | `linux-x86` | `linux-arm64` | `linux-arm` | `ios`   | `android` |
|-----------------|-----------|-----------|-------------|-------------|-----------|-------------|-------------|---------------|-------------|---------|-----------|
| `SDL3-CS`       | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     | &check; | &check;   |
| `SDL3_image-CS` | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     |         |           |
| `SDL3_ttf-CS`   | &check;   | &check;   | &check;     | &check;     | &check;   | &check;     | &check;     | &check;       | &check;     |         |           |

## Generating bindings

Bindings are generated via the provided Dockerfile:

```sh
docker build -t 'sdl-gen' .
docker run --rm -v .:/app -w /app -it sdl-gen
```

## License

This code is released under [MIT](LICENCE).
