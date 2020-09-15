using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Models;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Actions
{
    public class AddActionMainCommand : Command
    {
        private readonly DbUserService _dbUserService;
        private readonly DbActivitiesService _dbActivitiesService;
        private readonly DbUserActionsService _dbUserActionsService;
        public override string Name => "addActionMainCommand";

        public AddActionMainCommand(DbUserService dbUserService,
            DbActivitiesService dbActivitiesService, DbUserActionsService dbUserActionsService) : base(dbUserService)
        {
            _dbUserService = dbUserService;
            _dbActivitiesService = dbActivitiesService;
            _dbUserActionsService = dbUserActionsService;
        }
        public override async Task execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var user = _dbUserService.GetUserByChatId(chatId);
            if (user.LastCommandQuery.Contains("addActionCommand?name="))
            {
                try
                {
                    var activityName = user.LastCommandQuery.Substring(user.LastCommandQuery.IndexOf("addActionCommand?name=") + "addActionCommand?name=".Length);
                    var activity = _dbActivitiesService.GetActivity(user.Id, activityName);
                    var stringDateStart = message.Text.Substring(0, "dd.MM.yyyy/hh:mm".Length);
                    DateTime dateStart = DateTime.ParseExact(stringDateStart, "dd.MM.yyyy/hh:mm", CultureInfo.InvariantCulture);
                    var stringDateEnd = message.Text.Substring("dd.MM.yyyy/hh:mm-".Length, "dd.MM.yyyy/hh:mm".Length);
                    DateTime dateEnd = DateTime.ParseExact(stringDateEnd, "dd.MM.yyyy/hh:mm", CultureInfo.InvariantCulture);
                    if (dateEnd < dateStart) throw new Exception("dateEnd<dateStart");
                    TimeSpan timeSpent = dateEnd - dateStart;
                    UserAction userAction = new UserAction()
                    {
                        ActivityId = activity.Id,
                        UserId = user.Id,
                        TimeStart = dateStart,
                        TimeEnd = dateEnd,
                        TimeSpent = timeSpent
                    };
                    _dbUserActionsService.AddAction(userAction);
                    UpdateUserOnMessage(message);
                }
                catch(Exception e)
                {
                    await client.SendTextMessageAsync(chatId, "Exception in adding activity:"+e.Message);
                }
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "Error in activity(last message)");
            }
        }
    }
}
