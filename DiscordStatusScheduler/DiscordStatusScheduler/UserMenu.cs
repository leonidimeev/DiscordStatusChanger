namespace DiscordStatusScheduler;

public class UserMenu
{
    private readonly AppOptions _appOptions;

    public UserMenu(AppOptions appOptions)
    {
        _appOptions = appOptions;
    }

    public async Task Run()
    {
        while (true)
        {
            if (Program.Mode != null)
            {
                ConsoleSpiner spin = new ConsoleSpiner();
                Console.Write($"Running in {Program.Mode} mode ");
                while (true)
                {
                    spin.Turn();
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                }
            }
            else
            {
                // If not in a specific mode, pause for a short time without printing dots
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
        }
    }

    public class ConsoleSpiner
    {
        int counter;
        public ConsoleSpiner()  
        {
            counter = 0;
        }
        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}