namespace DiscordStatusScheduler;

public class AppOptions
{
    public const string Position = "App";

    public List<Mode> Modes { get; set; } = new List<Mode>();
}
