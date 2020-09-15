using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Activities.CallbackCommands
{
    public class RemoveActivityUtilityCommand:CallbackCommand
    {
        private readonly DbUserService _dbUserService;
        private readonly DbActivitiesService _dbActivitiesService;
        public override string Name => "removeActivityUtilityCommand";

        public RemoveActivityUtilityCommand(DbUserService dbUserService, DbActivitiesService dbActivitiesService) : base(dbUserService)
        {

            _dbUserService = dbUserService;
            _dbActivitiesService = dbActivitiesService;
        }

        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var chatId = query.Message.Chat.Id;
            var user = _dbUserService.GetUserByChatId(chatId);
            var activities = _dbActivitiesService.GetActivities(user.Id);
            var ikm = new List<InlineKeyboardButton>();
            activities.ForEach(a =>
            {
                ikm.Add(InlineKeyboardButton.WithCallbackData(a.Name, "removeActivityCommand?name="+a.Name));
            });
            var markup = new InlineKeyboardMarkup(ikm);
            await client.SendTextMessageAsync(chatId, "Choose activity to delete:",replyMarkup: markup);
        }
    }
}
