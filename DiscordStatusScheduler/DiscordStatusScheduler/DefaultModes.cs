
namespace DiscordStatusScheduler;

public class DefaultModes
{
    private static List<Mode> _defaultModes = new List<Mode>()
    {
        new Mode()
        {
            Name = "Default",
            StatusChangeFrequency = 1,
            StatusSet = new List<string>{ "Hello c:", "I am using DiscordStatusScheduler!"}
        }
    };

    public static void AddDefaultModes(AppOptions appOptions)
    {
        foreach (var mode in _defaultModes)
        {
            appOptions.UserOptions.Modes.Add(mode);
        }
    }
}
