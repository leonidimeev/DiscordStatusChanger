using DiscordStatusScheduler;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

class Program
{
    public static string Mode { get; set; } = default!;
    private AppOptions AppOptions { get; set; } = default!;
    private IConfigurationRoot Configuration { get; set; } = default!;

    static async Task Main(string[] args)
    {
        Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

        Program program = new Program();
        await program.RunAsync();
    }

    private async Task RunAsync()
    {
        // Build configuration
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .Build();

        // Configure AppOptions
        AppOptions = new AppOptions();
        var appOptionsConfigurationSectionValue = Configuration.GetSection(AppOptions.Position).Value;
        if (appOptionsConfigurationSectionValue != null)
        {
            AppOptions = JsonConvert.DeserializeObject<AppOptions>(appOptionsConfigurationSectionValue)!;
        }
        DefaultModes.AddDefaultModes(AppOptions);

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

        Environment.Exit(0);
    }
}
