using DiscordStatusScheduler;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

class Program
{
    public static string Mode { get; set; } = default!;
    public static AppOptions AppOptions { get; set; } = default!;
    public static IConfigurationRoot Configuration { get; set; } = default!;

    static async Task Main(string[] args)
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
        ProgramLogic programLogic = new ProgramLogic();
        UserInputHandler userInputHandler = new UserInputHandler();

        // Start tasks
        Task programLogicTask = Task.Run(() => programLogic.Run());
        Task userInputTask = Task.Run(() => userInputHandler.HandleInput());

        // Wait for tasks to complete
        await Task.WhenAll(programLogicTask, userInputTask);
    }

}
