using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTimeManagementBot.Models;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Commands.Actions.CallbackCommands
{
    public class AddActionCommand : CallbackCommand
    {
        private readonly DbUserActionsService _dbUserActionsService;
        private readonly DbUserService _dbUserService;
        private readonly DbActivitiesService _dbActivitiesService;
        public override string Name => "addActionCommand";

        public AddActionCommand(DbUserService dbUserService
            , DbUserActionsService dbUserActionsService, DbActivitiesService dbActivitiesService) : base(dbUserService)
        {
            _dbUserActionsService = dbUserActionsService;
            _dbUserService = dbUserService;
            _dbActivitiesService = dbActivitiesService;
        }

        public override async Task execute(CallbackQuery query, TelegramBotClient client)
        {
            UpdateUserOnMessage(query);
            var queryData = query.Data;
            var chatId = query.Message.Chat.Id;
            await client.SendTextMessageAsync(chatId, "Write your action in format dd.mm.yyyy/hh:mm-dd.mm.yyyy/hh:mm ,where first date is start, second - end.");
        }
    }
}
