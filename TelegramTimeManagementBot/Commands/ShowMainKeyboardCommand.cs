using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands
{
    public class ShowMainKeyboardCommand : Command
    {
        public override string Name => "showMainKeyboardCommand";

        public ShowMainKeyboardCommand(DbUserService dbUserService) : base(dbUserService) { }
        public override async Task execute(Message message, TelegramBotClient client)
        {
            UpdateUserOnMessage(message);
            var markup = new ReplyKeyboardMarkup(new[]{new KeyboardButton[] {
                                                    new KeyboardButton("Cancel"),
                                                },
                                                new KeyboardButton[]
                                                {
                                                    new KeyboardButton("start")
                                                },
                                                new KeyboardButton[]
                                                {
                                                    new KeyboardButton("showActionsMenuCommand")
                                                },
                                                new KeyboardButton[]
                                                {
                                                    new KeyboardButton("showActivitiesMenuCommand")
                                                }
            });
            await client.SendTextMessageAsync(message.Chat.Id, "Keyboard is active", replyMarkup: markup);
        }
    }
}
