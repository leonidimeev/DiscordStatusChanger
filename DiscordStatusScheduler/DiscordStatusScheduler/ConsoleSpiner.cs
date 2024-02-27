namespace DiscordStatusScheduler
{
    public class ConsoleSpiner
    {
        private int _currentPosition;
        private readonly string[] Spinners = { "/", "-", "\\", "|" };
        private readonly string StopMessage = "STOPPED";

        public ConsoleSpiner()
        {
            _currentPosition = 0;
        }

        public void Turn()
        {
            _currentPosition = (_currentPosition + 1) % Spinners.Length;

            // Print the spinner at the end of the line
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);
            Console.Write(Spinners[_currentPosition]);

            // Move the cursor back to the original position
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);
        }

        public void Stop()
        {
            _currentPosition = (_currentPosition + 1) % StopMessage.Length;

            // Print the spinner at the start of the stop message
            Console.SetCursorPosition(Console.WindowWidth - StopMessage.Length, Console.CursorTop);
            Console.Write(StopMessage);

            // Move the cursor back to the original position
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);
        }
    }
}
