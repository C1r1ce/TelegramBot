using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Models;
using TelegramTimeManagementBot.Services.Db;
using User = TelegramTimeManagementBot.Models.User;

namespace TelegramTimeManagementBot.Commands.Activities.CallbackCommands
{
    public class ShowActivitiesCommand : CallbackCommand
    {
        private readonly DbActivitiesService _dbActivitiesService;
        private readonly DbUserService _dbUserService;
        public override string Name => "showActivitiesCommand";

        public ShowActivitiesCommand(DbActivitiesService dbActivitiesService, DbUserService dbUserService):base(dbUserService)
        {
            _dbActivitiesService = dbActivitiesService;
            _dbUserService = dbUserService;
        }

        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var chatId = query.Message.Chat.Id;
            User user = _dbUserService.GetUserByChatId(chatId);
            var activities = _dbActivitiesService.GetActivities(user.Id);
            string text = "Your activities:\n";
            activities.ForEach(a => text += a.Name + "\n");
            await client.SendTextMessageAsync(chatId, text);
        }
    }
}
