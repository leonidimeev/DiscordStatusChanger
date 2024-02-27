using DiscordStatusScheduler;
using Microsoft.Extensions.Configuration;
using System.Reflection;

class Program
{
    public static Mode Mode { get; set; } = default!;
    public static bool SwitchingStatusProcessing { get; set; } = false;
    private AppOptions AppOptions { get; set; } = default!;
    private IConfigurationRoot Configuration { get; set; } = default!;

    static async Task Main(string[] args)
    {
        Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress!);

        Program program = new Program();
        await program.RunAsync();
    }

    private async Task RunAsync()
    {
        // Get the directory containing the compiled DLL file
        string dllDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (dllDirectory == null)
        {
            // Handle the case where the directory cannot be determined
            throw new InvalidOperationException("Unable to determine the directory of the compiled DLL file.");
        }

        // Build configuration
        Configuration = new ConfigurationBuilder()
            .SetBasePath(dllDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .Build();

        // Configure AppOptions
        var appOptions = ConfigurationBinder.Get<AppOptions>(Configuration.GetSection("App"));
        if (appOptions != null)
        {
            AppOptions = appOptions;
        }

        // Instantiate classes
        App app = new App(AppOptions);
        UserMenu userMenu = new UserMenu(AppOptions);
        UserInputHandler userInputHandler = new UserInputHandler(AppOptions);

        // Start tasks
        Task programLogicTask = Task.Run(() => app.Run());
        Task programUserMenu = Task.Run(() => userMenu.Run());
        Task userInputTask = Task.Run(() => userInputHandler.HandleMode());

        // Wait for tasks to complete
        await Task.WhenAll(programLogicTask, userInputTask);
    }

    static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
        Console.WriteLine("Ctrl+C detected. Restoring to the original state...");

        SwitchingStatusProcessing = false;

        Environment.Exit(0);
    }
}
