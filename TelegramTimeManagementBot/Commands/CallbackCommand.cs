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
    public abstract class CallbackCommand
    {
        private readonly DbUserService _dbUserService;
        public abstract string Name { get; }
        public CallbackCommand(DbUserService dbUserService)
        {
            _dbUserService = dbUserService;
        }

        public abstract Task execute(CallbackQuery query, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(Name);
        }

        public void UpdateUserOnMessage(CallbackQuery query)
        {
            var user = _dbUserService.GetUserByChatId(query.Message.Chat.Id);
            user.LastMessageTime = DateTime.Now;
            user.LastCommandQuery = query.Data;
            _dbUserService.UpdateUser(user);
        }
    }
}
