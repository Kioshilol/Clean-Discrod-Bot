using SmartDiscordBot.Bot.Modules;
using SmartDiscordBot.Models;
using System.Threading.Tasks;

namespace SmartDiscordBot.Services.Interfaces
{
    public interface IApiController
    {
        Task<OperationResult<MemoModel>> GetRedditMemo();
    }
}
