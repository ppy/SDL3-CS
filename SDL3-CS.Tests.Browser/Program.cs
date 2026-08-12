using System.Threading.Tasks;

internal class Program
{
    // async Task is necessary, or the browser will be frozen
    public static async Task Main(string[] args)
    {
        await SDL.Tests.Program.MainAsync();
    }
}
