namespace DiscordStatusScheduler;

public class App
{
    private readonly AppOptions _appOptions;

    static string[] status_set = { "online", "idle", "dnd" };
    static int status_selector = 0;

    public App(AppOptions appOptions)
    {
        _appOptions = appOptions;
    }

    public async Task Run()
    {
        DiscordClient discordClient = new DiscordClient(_appOptions);

        while (true)
        {
            if (Program.SwitchingStatusProcessing)
            {
                foreach (var status in Program.Mode.StatusSet)
                {
                    var statusTextChangeResponse = await discordClient.StatusTextChangeAsync(status, status_set[status_selector]);
                    status_selector += 1;
                    if (status_selector == status_set.Length)
                    {
                        status_selector = 0;
                    }
                }
            }
        }
    }
}
