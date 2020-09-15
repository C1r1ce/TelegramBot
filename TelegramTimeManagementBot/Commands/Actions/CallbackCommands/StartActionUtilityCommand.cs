using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Actions.CallbackCommands
{
    public class StartActionUtilityCommand : CallbackCommand
    {
        private readonly DbActivitiesService _dbActivitiesService;
        private readonly DbUserService _dbUserService;
        public override string Name => "startActionUtilityCommand";

        public StartActionUtilityCommand(DbUserService dbUserService, DbActivitiesService dbActivitiesService) : base(dbUserService) 
        {
            _dbActivitiesService = dbActivitiesService;
            _dbUserService = dbUserService;
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
                ikm.Add(InlineKeyboardButton.WithCallbackData(a.Name, "startActionCommand?name=" + a.Name));
            });
            var markup = new InlineKeyboardMarkup(ikm);
            await client.SendTextMessageAsync(chatId, "Choose activity to start", replyMarkup:markup);
        }
    }
}
