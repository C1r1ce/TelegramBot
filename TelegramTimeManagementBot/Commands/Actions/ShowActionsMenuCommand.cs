using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Actions
{
    public class ShowActionsMenuCommand : Command
    {
        public override string Name => "showActionsMenuCommand";

        public ShowActionsMenuCommand(DbUserService dbUserService) : base(dbUserService) { }
        public override async Task execute(Message message, TelegramBotClient client)
        {
            UpdateUserOnMessage(message);
            var markup = new InlineKeyboardMarkup(new[]{new InlineKeyboardButton[] {
                                                    InlineKeyboardButton.WithCallbackData("StartAction","startActionUtilityCommand")
                                                },
                                                new InlineKeyboardButton[] {
                                                    InlineKeyboardButton.WithCallbackData("EndAction","endActionCommand")
                                                },
                                                new InlineKeyboardButton[] {
                                                    InlineKeyboardButton.WithCallbackData("AddAction","addActionUtilityCommand")
                                                },
                                                new InlineKeyboardButton[] {
                                                    InlineKeyboardButton.WithCallbackData("RemoveAction","removeActionUtilityCommand")
                                                },
                                                new InlineKeyboardButton[] {
                                                    InlineKeyboardButton.WithCallbackData("ShowActions","showActionsUtilityCommand")
                                                } });
            await client.SendTextMessageAsync(message.Chat.Id, "Choose action", replyMarkup: markup);
        }
    }
}
