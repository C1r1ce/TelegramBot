using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Actions.CallbackCommands
{
    public class ShowActionsMainCommand : CallbackCommand
    {
        private readonly DbUserActionsService _dbUserActionsService;
        private readonly DbActivitiesService _dbActivitiesService;
        private readonly DbUserService _dbUserService;
        public override string Name => "showActionsMainCommand";

        public ShowActionsMainCommand(DbUserService dbUserService,
            DbUserActionsService dbUserActionsService, DbActivitiesService dbActivitiesService) : base(dbUserService)
        {
            _dbUserActionsService = dbUserActionsService;
            _dbUserService = dbUserService;
            _dbActivitiesService = dbActivitiesService;
        }
        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var chatId = query.Message.Chat.Id;
            var user = _dbUserService.GetUserByChatId(chatId);
            var activityName = query.Data.Substring(query.Data.IndexOf("?name=") + "?name=".Length,
                query.Data.IndexOf(",time=") - query.Data.IndexOf("?name=") - "?name=".Length);
            var activity = _dbActivitiesService.GetActivity(user.Id, activityName);
            var timeInQuery = query.Data.Substring(query.Data.IndexOf("time=")+ "time=".Length);
            TimeSpan timeSpent;
            if (timeInQuery.Equals("all"))
            {
                timeSpent = _dbUserActionsService.GetAllTimeSpent(activity.Id);
            }
            else
            {
                int days = Int32.Parse(timeInQuery);
                timeSpent = _dbUserActionsService.GetLastTimeSpent(activity.Id, DateTime.Now.AddDays(0 - days));
            }
            await client.SendTextMessageAsync(chatId, "Activity: " + activityName + " time spent: " + timeSpent.Days + "dd " + timeSpent.Hours + "hh " + timeSpent.Minutes + "mm");
        }
    }
}
