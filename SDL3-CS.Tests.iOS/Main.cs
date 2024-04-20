using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL;

public class Program
{
    public static unsafe void Main(string[] args)
    {
        NativeLibrary.SetDllImportResolver(typeof(SDL3).Assembly, (_, assembly, path) => NativeLibrary.Load("@rpath/SDL3.framework/SDL3", assembly, path));

        SDL3.SDL_RunApp(0, null, &main, IntPtr.Zero);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe int main(int argc, byte** argv)
    {
        SDL.Tests.Program.Main();
        return 0;
    }
}
