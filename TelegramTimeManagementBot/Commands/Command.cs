using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands
{
    public abstract class Command
    {
        private readonly DbUserService _dbUserService;
        public abstract string Name { get; }

        public Command(DbUserService dbUserService)
        {
            _dbUserService = dbUserService;
        }
        public abstract Task execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(Name);
        }
        public void UpdateUserOnMessage(Message message)
        {
            var user = _dbUserService.GetUserByChatId(message.Chat.Id);
            user.LastMessageTime = DateTime.Now;
            user.LastCommandQuery = message.Text;
            _dbUserService.UpdateUser(user);
        }
    }
}
