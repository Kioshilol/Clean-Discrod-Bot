using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDiscordBot.Models
{
    public class OperationResult<T>
    {
        public bool IsSucces { get; set; }
        public T ResponseData { get; set; }
    }
}
