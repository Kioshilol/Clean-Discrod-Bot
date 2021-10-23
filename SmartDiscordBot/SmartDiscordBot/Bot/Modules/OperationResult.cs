using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDiscordBot.Bot.Modules
{
    public class OperationResult<T>
    {
        public bool IsSucces { get; set; }
        public T ResponseData { get; set; }
    }
}
