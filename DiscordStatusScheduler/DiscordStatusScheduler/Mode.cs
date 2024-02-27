namespace DiscordStatusScheduler;

public class Mode
{
    public required string Name { get; set; }
    public int StatusChangeFrequency { get; set; }
    public List<string>? StatusSet { get; set; }
}
