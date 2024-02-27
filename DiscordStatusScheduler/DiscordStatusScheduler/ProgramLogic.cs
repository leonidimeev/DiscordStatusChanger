namespace DiscordStatusScheduler;

internal class ProgramLogic
{
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
