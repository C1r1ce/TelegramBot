using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramTimeManagementBot.Services
{
    public interface IUpdateService
    {
        Task ExecuteUpdate(Update update);
    }
}
