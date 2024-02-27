namespace DiscordStatusScheduler;

internal class UserInputHandler
{
    private readonly AppOptions _appOptions;

    public UserInputHandler(AppOptions appOptions)
    {
        _appOptions = appOptions;
    }

    public void HandleInput()
    {
        while (true)
        {
            string userInput = Console.ReadLine();

            if (userInput != null)
            {
                if (userInput.ToLower() == "exit")
                {
                    Program.Mode = DefaultModes.ExitMode;
                }

                var switchingMode = _appOptions.Modes.FirstOrDefault(x => x.Name.ToLower() == userInput.ToLower());

                if (switchingMode != null)
                {
                    Console.WriteLine($"Switching to {userInput} mode...");
                    Program.Mode = userInput;
                }
                else
                {
                    Console.WriteLine($"Mode {userInput} not found!");
                }
            }
        }
    }
}
