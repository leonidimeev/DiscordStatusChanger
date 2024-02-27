namespace DiscordStatusScheduler;

internal class App
{
    private readonly AppOptions _appOptions;

    public App(AppOptions appOptions)
    {
        _appOptions = appOptions;
    }

    public async Task Run()
    {
        while (true)
        {
            if (Program.Mode != null)
            {
                Console.WriteLine($"Running in {Program.Mode} mode...");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
