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
    public class ShowActionsCommand : CallbackCommand
    {
        public override string Name => "showActionsCommand";

        public ShowActionsCommand(DbUserService dbUserService) : base(dbUserService) { }
        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var chatId = query.Message.Chat.Id;
            var activityName = query.Data.Substring("showActionsCommand?name=".Length);
            var markup = new InlineKeyboardMarkup(new InlineKeyboardButton[] {
                                                    InlineKeyboardButton.WithCallbackData("All time","showActionsMainCommand?name="+activityName +",time=all"),
                                                    InlineKeyboardButton.WithCallbackData("30 days","showActionsMainCommand?name="+activityName +",time=30"),
                                                    InlineKeyboardButton.WithCallbackData("15 days","showActionsMainCommand?name="+activityName +",time=15"),
                                                    InlineKeyboardButton.WithCallbackData("7 days","showActionsMainCommand?name="+activityName +",time=7"),
                                                    InlineKeyboardButton.WithCallbackData("1 day","showActionsMainCommand?name="+activityName +",time=1")
                                                });
            await client.SendTextMessageAsync(chatId, "Choose time interval", replyMarkup: markup);
        }
    }
}
