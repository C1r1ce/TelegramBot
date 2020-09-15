using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Models;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Actions.CallbackCommands
{
    public class StartActionCommand : CallbackCommand
    {
        private readonly DbUserService _dbUserService;
        private readonly DbUserActionsService _dbUserActionsService;
        private readonly DbActivitiesService _dbActivitiesService;
        public override string Name => "startActionCommand";

        public StartActionCommand(DbUserService dbUserService,
            DbUserActionsService dbUserActionsService, DbActivitiesService dbActivitiesService) : base(dbUserService)
        {
            _dbActivitiesService = dbActivitiesService;
            _dbUserActionsService = dbUserActionsService;
            _dbUserService = dbUserService;
        }

        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var activityName = query.Data.Substring("showActionsCommand?name=".Length);
            var chatId = query.Message.Chat.Id;
            var user = _dbUserService.GetUserByChatId(chatId);
            var activity = _dbActivitiesService.GetActivity(user.Id, activityName);
            UserAction userAction = new UserAction() {
                ActivityId = activity.Id,
                TimeStart = DateTime.Now,
                UserId = user.Id 
            };
            var activeAction = _dbUserActionsService.GetLastActiveActionByUserId(user.Id);
            if(activeAction == null)
            {
                _dbUserActionsService.AddAction(userAction);
                await client.SendTextMessageAsync(chatId, "Action started");
            }
            else
            {
                var activeActivity = _dbActivitiesService.GetActivity(activeAction.ActivityId);
                await client.SendTextMessageAsync(chatId, "You have already started action " + activeActivity.Name);
            }
            
        }
    }
}
