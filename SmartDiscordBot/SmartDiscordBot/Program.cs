using System;

namespace SmartDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello bro!");
            var startupService = SetupStartupService.Setup();
            startupService.RunAsync().GetAwaiter().GetResult();
        }
    }
}
