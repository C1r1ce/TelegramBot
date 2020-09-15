using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Activities
{
    public class ShowActivitiesMenuCommand : Command
    {
        public override string Name => "showActivitiesMenuCommand";

        public ShowActivitiesMenuCommand(DbUserService dbUserService) : base(dbUserService) { }
        public override async Task execute(Message message, TelegramBotClient client)
        {
            UpdateUserOnMessage(message);
            var markup = new InlineKeyboardMarkup(new[]{new InlineKeyboardButton[] {
                                                    InlineKeyboardButton.WithCallbackData("AddActivity","addActivityUtilityCommand")
                                                },
                                                new InlineKeyboardButton[]{
                                                    InlineKeyboardButton.WithCallbackData("RemoveActivity","removeActivityUtilityCommand")
                                                },
                                                new InlineKeyboardButton[]{
                                                    InlineKeyboardButton.WithCallbackData("ShowActivities","showActivitiesCommand")
                                                }
            });
            await client.SendTextMessageAsync(message.Chat.Id, "Choose action", replyMarkup: markup);
        }
    }
}
