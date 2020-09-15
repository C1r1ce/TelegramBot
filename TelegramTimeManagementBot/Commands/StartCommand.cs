using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Services.Db;
using User = TelegramTimeManagementBot.Models.User;

namespace TelegramTimeManagementBot.Commands
{
    public class StartCommand : Command
    {
        private readonly DbUserService _dbUserServices;
        public override string Name => "start";

        public StartCommand(DbUserService dbUserServices):base(dbUserServices)
        {
            _dbUserServices = dbUserServices;
        }

        public override async Task execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            User user = new User() { ChatId = chatId, IsActive = false, LastMessageTime = DateTime.Now };
            if (!_dbUserServices.IsUserExist(user))
            {
                _dbUserServices.AddUser(user);
                await client.SendTextMessageAsync(chatId, "Now you can start using the bot");
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "You are already registered and can use the bot");
            }
            UpdateUserOnMessage(message);
        }
    }
}
