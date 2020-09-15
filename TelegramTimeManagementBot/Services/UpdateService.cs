using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramTimeManagementBot.Commands;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;
        private readonly CommandService _commandService;
        private readonly DbUserService _dbUserService;

        public UpdateService(IBotService botService, CommandService commandService, DbUserService dbUserService)
        {
            _botService = botService;
            _commandService = commandService;
            _dbUserService = dbUserService;
        }

        public async Task ExecuteUpdate(Update update)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            if (update.Message.Text.Equals("Cancel"))
                            {
                                foreach (Command command in _commandService.GetCommands())
                                {
                                    if (command.Contains("showMainKeyboardCommand"))
                                    {
                                        await command.execute(update.Message, _botService.Client);
                                    }
                                }
                            }
                            else
                            if (_dbUserService.GetUserByChatId(update.Message.Chat.Id) != null &&
                                    _dbUserService.GetUserByChatId(update.Message.Chat.Id).LastCommandQuery.Contains("addActionCommand?name="))
                            {
                                foreach (Command command in _commandService.GetCommands())
                                {
                                    if (command.Contains("addActionMainCommand"))
                                    {
                                        await command.execute(update.Message, _botService.Client);
                                    }
                                }
                            }
                            else
                            if(_dbUserService.GetUserByChatId(update.Message.Chat.Id) != null &&
                                    _dbUserService.GetUserByChatId(update.Message.Chat.Id).LastCommandQuery.Contains("addActivityUtilityCommand"))
                            {
                                foreach (Command command in _commandService.GetCommands())
                                {
                                    if (command.Contains("addActivityCommand"))
                                    {
                                        await command.execute(update.Message, _botService.Client);
                                    }
                                }
                            }
                            else
                            {
                                int count = 0;
                                foreach (Command command in _commandService.GetCommands())
                                {
                                    count++;
                                    if (command.Contains(update.Message.Text))
                                    {
                                        await command.execute(update.Message, _botService.Client);
                                        break;
                                    }
                                    if (_commandService.GetCommands().Count == count)
                                    {
                                        await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "Unknown command");
                                    }
                                }
                            }
                            break;
                        }
                    case UpdateType.CallbackQuery:
                        {
                            int countQ = 0;
                            foreach (CallbackCommand command in _commandService.GetCallbackCommands())
                            {
                                countQ++;
                                if (command.Contains(update.CallbackQuery.Data))
                                {
                                    await command.execute(update.CallbackQuery, _botService.Client);
                                    break;
                                }
                                if (_commandService.GetCallbackCommands().Count == countQ)
                                {
                                    await _botService.Client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Unknown callbackCommand");
                                }
                            }
                            break;
                        }
                }
                
                
            }
            catch(Exception e)
            {
                await _botService.Client.SendTextMessageAsync(472684743, "Exeption " + e.Message + "/n" + e.StackTrace);
            }
            
        }
    }
}
