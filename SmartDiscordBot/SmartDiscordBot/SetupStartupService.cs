using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartDiscordBot.Bot;
using SmartDiscordBot.Interfaces;
using SmartDiscordBot.ManualBot;
using SmartDiscordBot.Models;
using System.IO;

namespace SmartDiscordBot
{
    public class SetupStartupService
    {
        public static IStartup Setup()
        {
            var services = new ServiceCollection();
            var isManualMode = IsManualMode();

            if (isManualMode)
            {
                ManualStartup.ConfigureServices(services);
                services.AddSingleton<IStartup, ManualStartup>();
            }
            else
            {
                services.AddSingleton<IStartup, Startup>();
            }

            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<IStartup>();
        }

        private static bool IsManualMode()
        {
            ApplicationSettingsModel model = new ApplicationSettingsModel();

            using (FileStream fileStream = new FileStream("appconfig.json", FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    var qwe = streamReader.ReadToEnd();
                    model = JsonConvert.DeserializeObject<ApplicationSettingsModel>(qwe);
                }
            }

            return model.IsManual;
        }
    }
}
