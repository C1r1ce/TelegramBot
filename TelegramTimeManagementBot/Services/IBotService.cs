using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramTimeManagementBot.Services
{
    public interface IBotService
    {
        public TelegramBotClient Client { get; }
    }
}
