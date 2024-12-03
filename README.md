# SDL3-CS

SDL3-CS is [SDL3](https://github.com/libsdl-org/SDL) bindings, developed for internal use and available publicly on [NuGet.org](https://www.nuget.org/packages/ppy.SDL3-CS).

## About

The library is functional and available for public use. While it is actively maintained, updates are primarily driven by our internal needs. Please set your expectations accordingly when using or adapting SDL3-CS in your own projects.

Contributions to keep the bindings up-to-date with upstream SDL3 changes are welcome. If you have improvements or updates, feel free to submit a pull request.

## Generating Bindings

Bindings should be generated via the provided Dockerfile:

```sh
docker build -t 'sdl-gen' .
docker run --rm -v .:/app -w /app -it sdl-gen
```

## License

This code is released under [MIT](LICENCE).
