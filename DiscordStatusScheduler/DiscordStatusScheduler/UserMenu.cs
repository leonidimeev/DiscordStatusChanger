namespace DiscordStatusScheduler;

public class UserMenu
{
    private readonly AppOptions _appOptions;

    private bool _isSpinnerEnabled = true;

    public UserMenu(AppOptions appOptions)
    {
        _appOptions = appOptions;
    }

    public async Task Run()
    {
        Console.Write(LogoArt());
        Console.Write(WelcomeInfo());
        Console.Write(AvailableModesInfo());

        while (true)
        {
            HandleMode();

            if (Program.Mode != null)
            {
                await RunMode();
            }
        }
    }

    private async Task RunMode()
    {
        ConsoleSpiner spinner = new ConsoleSpiner();
        Console.WriteLine($"Press CTRL-C to exit");
        Console.WriteLine($"Press CTRL-SPACE to switch mode");
        Console.Write($"Running in {Program.Mode.Name} mode");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.Spacebar)
                {
                    spinner.Stop();
                    Program.SwitchingStatusProcessing = false;
                    HandleMode();
                }
            }

            if (_isSpinnerEnabled)
            {
                spinner.Turn();
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }
        }
    }

    private void HandleMode()
    {
        Console.WriteLine();
        Console.Write("Please enter the desired operating mode of the program: ");
        string userInput = Console.ReadLine()!;

        if (userInput != null)
        {
            var switchingMode = _appOptions.UserOptions.Modes.FirstOrDefault(x => x.Name.ToLower() == userInput.ToLower());

            if (switchingMode != null)
            {
                Console.WriteLine($"Switching to {userInput} mode...");
                Program.Mode = switchingMode;
                Program.SwitchingStatusProcessing = true;
            }
            else
            {
                Console.WriteLine($"Mode {userInput} not found!");
            }
        }
    }

    private string LogoArt()
    {
        return 
            "░▒▓███████▓▒░ ░▒▓███████▓▒░▒▓███████▓▒░ \r\n" +
            "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░     ░▒▓█▓▒░        \r\n" +
            "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░     ░▒▓█▓▒░        \r\n" +
            "░▒▓█▓▒░░▒▓█▓▒░░▒▓██████▓▒░░▒▓██████▓▒░  \r\n" +
            "░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░     ░▒▓█▓▒░ \r\n" +
            "░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░     ░▒▓█▓▒░ \r\n" +
            "░▒▓███████▓▒░░▒▓███████▓▒░▒▓███████▓▒░  \r\n" +
            "                                        \r\n";
    }

    private string WelcomeInfo()
    {
        return
            "Welcome to Discord Status Changer v1.0!\r\n" +
            "\r\n" +
            "This console application provides a convenient way to change user statuses on Discord.\r\n";
    }

    private string AvailableModesInfo()
    {
        var message = "Available modes: \n";
        if (_appOptions.UserOptions.Modes.Count != 0)
        {
            foreach (var mode in _appOptions.UserOptions.Modes)
            {
                message += mode.Name + "\n";
            }
        }
        else
        {
            message += "No any modes";
        }
        return message;
    }
}