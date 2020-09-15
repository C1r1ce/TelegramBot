using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Services.Db;
using User = TelegramTimeManagementBot.Models.User;

namespace TelegramTimeManagementBot.Commands.Activities.CallbackCommands
{
    public class AddActivityUtilityCommand : CallbackCommand
    {
        private readonly DbUserService _dbUserService;
        private readonly DbActivitiesService _dbActivitiesService;
        public override string Name => "addActivityUtilityCommand";

        public AddActivityUtilityCommand(DbUserService dbUserService, DbActivitiesService dbActivitiesService):base(dbUserService)
        {
            
            _dbUserService = dbUserService;
            _dbActivitiesService = dbActivitiesService;
        }

        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var chatId = query.Message.Chat.Id;
            await client.SendTextMessageAsync(chatId, "Enter name of activity");
        }
    }
}
