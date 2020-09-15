using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Actions.CallbackCommands
{
    public class EndActionCommand : CallbackCommand
    {
        private readonly DbUserService _dbUserService;
        private readonly DbUserActionsService _dbUserActionsService;
        public override string Name => "endActionCommand";

        public EndActionCommand(DbUserService dbUserService, DbUserActionsService dbUserActionsService) : base(dbUserService)
        {
            _dbUserService = dbUserService;
            _dbUserActionsService = dbUserActionsService;
        }
        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var chatId = query.Message.Chat.Id;
            var user = _dbUserService.GetUserByChatId(chatId);
            var action = _dbUserActionsService.GetLastActiveActionByUserId(user.Id);
            if(action != null)
            {
                action.TimeEnd = DateTime.Now;
                action.TimeSpent = action.TimeEnd - action.TimeStart;
                _dbUserActionsService.UpdateAction(action);
                await client.SendTextMessageAsync(chatId, "Time spent " + action.TimeSpent.Hours + "h " + action.TimeSpent.Minutes + "m");
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "You don`t have any active actions");
            }
           
        }
    }
}
