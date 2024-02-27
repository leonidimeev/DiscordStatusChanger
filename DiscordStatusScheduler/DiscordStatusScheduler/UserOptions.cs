namespace DiscordStatusScheduler;

public class UserOptions
{
    public string AuthToken { get; set; }
    public List<Mode> Modes { get; set; } = new List<Mode>();
}
