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
        Program program = new Program();
        await program.RunAsync();
    }

    private async Task RunAsync()
    {
        // Build configuration
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        // Configure AppOptions
        AppOptions = new AppOptions();
        AppOptions = JsonConvert.DeserializeObject<AppOptions>(Configuration.GetSection(AppOptions.Position).Value!)!;

        // Instantiate classes
        App app = new App(AppOptions);
        UserInputHandler userInputHandler = new UserInputHandler(AppOptions);

        // Start tasks
        Task programLogicTask = Task.Run(() => app.Run());
        Task userInputTask = Task.Run(() => userInputHandler.HandleInput());

        // Wait for tasks to complete
        await Task.WhenAll(programLogicTask, userInputTask);
    }

}
