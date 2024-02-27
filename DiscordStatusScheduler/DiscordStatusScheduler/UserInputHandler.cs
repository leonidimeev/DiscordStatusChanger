namespace DiscordStatusScheduler;

internal class UserInputHandler
{
    public void HandleInput()
    {
        while (true)
        {
            string userInput = Console.ReadLine();

            if (!string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine($"Switching to {userInput} mode...");
                Program.Mode = userInput;
            }
        }
    }
}
