using Java.Lang;
using Org.Libsdl.App;

namespace SDL.Tests.Android
{
    [Activity(Label = "SDL3-CS Android Tests", MainLauncher = true)]
    public class MainActivity : SDLActivity
    {
        protected override string[] GetLibraries() => ["SDL3"];

        protected override IRunnable CreateSDLMainRunnable() => new Runnable(Program.Main);
    }
}
