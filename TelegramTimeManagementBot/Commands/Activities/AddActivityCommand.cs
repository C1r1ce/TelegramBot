using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Models;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Activities
{
    public class AddActivityCommand : Command
    {
        private readonly DbActivitiesService _dbActivitiesService;
        private readonly DbUserService _dbUserService;
        public AddActivityCommand(DbUserService dbUserService, DbActivitiesService dbActivitiesService) : base(dbUserService)
        {
            _dbActivitiesService = dbActivitiesService;
            _dbUserService = dbUserService;
        }
        public override string Name => "addActivityCommand";

        public override async Task execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var activityName = message.Text;
            var user = _dbUserService.GetUserByChatId(chatId);
            Activity activity = new Activity() { Name = activityName, UserId = user.Id };
            _dbActivitiesService.AddActivity(activity);
            UpdateUserOnMessage(message);
            await client.SendTextMessageAsync(chatId, "Success");
        }
    }
}
