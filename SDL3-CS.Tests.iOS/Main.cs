using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL;

public class Program
{
    public static unsafe void Main(string[] args)
    {
        NativeLibrary.SetDllImportResolver(typeof(SDL3).Assembly, (_, assembly, path) => NativeLibrary.Load("@rpath/SDL3.framework/SDL3", assembly, path));
        NativeLibrary.SetDllImportResolver(typeof(SDL3_image).Assembly, (_, assembly, path) => NativeLibrary.Load("@rpath/SDL3_image.framework/SDL3_image", assembly, path));
        NativeLibrary.SetDllImportResolver(typeof(SDL3_ttf).Assembly, (_, assembly, path) => NativeLibrary.Load("@rpath/SDL3_ttf.framework/SDL3_ttf", assembly, path));
        NativeLibrary.SetDllImportResolver(typeof(SDL3_mixer).Assembly, (_, assembly, path) => NativeLibrary.Load("@rpath/SDL3_mixer.framework/SDL3_mixer", assembly, path));

        SDL3.SDL_RunApp(0, null, &main, IntPtr.Zero);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe int main(int argc, byte** argv)
    {
        SDL.Tests.Program.Main();
        return 0;
    }
}
