namespace DiscordStatusScheduler;

public class Mode
{
    public required string Name { get; set; }
    public int StatusChangeFrequency { get; set; } = default!;
    public List<string> StatusSet { get; set; } = new List<string>();
}
