using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramTimeManagementBot.Services
{
    public class BotService : IBotService
    {
        private readonly BotOptions _botOptions;
        public BotService(IOptions<BotOptions> botOptions)
        {
            _botOptions = botOptions.Value;
            Client = new TelegramBotClient(_botOptions.Token);
        }

        public TelegramBotClient Client { get; }

    }
}
