using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartDiscordBot.Models;
using SmartDiscordBot.Services.Interfaces;
using System.Threading.Tasks;

namespace SmartDiscordBot.Services
{
    public class ApiController : BaseApiController, IApiController
    {
        public async Task<OperationResult<MemoModel>> GetRedditMemo()
        {
            var operationModel = new OperationResult<MemoModel>();
            string responseBody = await GetRequest(Constants.RedditGetMemoUrl);

            if(responseBody != null)
            {
                JArray array = JArray.Parse(responseBody);

                try
                {
                    JObject meme = JObject.Parse(array[0]["data"]["children"][0]["data"].ToString());
                    operationModel.ResponseData = JsonConvert.DeserializeObject<MemoModel>(meme.ToString());
                }
                catch
                {

                }
            }

            operationModel.IsSucces = operationModel.ResponseData != null;
            return operationModel;
        }
    }
}
