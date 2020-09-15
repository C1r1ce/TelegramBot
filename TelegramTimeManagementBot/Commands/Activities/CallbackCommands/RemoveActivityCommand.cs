using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Activities.CallbackCommands
{
    public class RemoveActivityCommand : CallbackCommand
    {
        private readonly DbUserService _dbUserService;
        private readonly DbActivitiesService _dbActivitiesService;
        public override string Name => "removeActivityCommand";

        public RemoveActivityCommand(DbUserService dbUserService, DbActivitiesService dbActivitiesService) : base(dbUserService)
        {

            _dbUserService = dbUserService;
            _dbActivitiesService = dbActivitiesService;
        }
        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var queryData = query.Data;
            var chatId = query.Message.Chat.Id;
            var user = _dbUserService.GetUserByChatId(chatId);
            var activityName = queryData.Substring("removeActivityCommand?name=".Length);
            var activity = _dbActivitiesService.GetActivity(user.Id, activityName);
            _dbActivitiesService.RemoveActivity(activity);
            await client.SendTextMessageAsync(chatId, "Success");
        }
    }
}
