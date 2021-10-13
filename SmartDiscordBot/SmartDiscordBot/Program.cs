using System;

namespace SmartDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello bro!");
            var startup = new Startup();
            startup.RunAsync().GetAwaiter().GetResult();
        }
    }
}
