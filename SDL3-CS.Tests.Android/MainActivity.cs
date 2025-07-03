using Org.Libsdl.App;

namespace SDL.Tests.Android
{
    [Activity(Label = "SDL3-CS Android Tests", MainLauncher = true)]
    public class MainActivity : SDLActivity
    {
        protected override string[] GetLibraries() => ["SDL3", "SDL3_image", "SDL3_ttf", "SDL3_mixer"];

        protected override void Main() => Program.Main();
    }
}
