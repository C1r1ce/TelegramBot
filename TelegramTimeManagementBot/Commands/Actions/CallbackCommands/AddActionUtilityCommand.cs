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
    public class AddActionUtilityCommand : CallbackCommand
    {
        private readonly DbActivitiesService _dbActivitiesService;
        private readonly DbUserService _dbUserService;
        public override string Name => "addActionUtilityCommand";

        public AddActionUtilityCommand(DbActivitiesService dbActivitiesService, DbUserService dbUserService) : base(dbUserService)
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
                ikm.Add(InlineKeyboardButton.WithCallbackData(a.Name, "addActionCommand?name=" + a.Name));
            });
            var markup = new InlineKeyboardMarkup(ikm);
            await client.SendTextMessageAsync(chatId, "Choose action", replyMarkup:markup);
        }
    }
}
