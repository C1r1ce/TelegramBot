using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using TelegramTimeManagementBot.Commands;
using TelegramTimeManagementBot.Commands.Actions;
using TelegramTimeManagementBot.Commands.Actions.CallbackCommands;
using TelegramTimeManagementBot.Commands.Activities;
using TelegramTimeManagementBot.Commands.Activities.CallbackCommands;
using TelegramTimeManagementBot.Services.Db;

namespace TelegramTimeManagementBot.Services
{
    public class CommandService
    {
        private readonly DbActivitiesService _dbActivitiesService;
        private readonly DbUserActionsService _dbUserActionsService;
        private readonly DbUserService _dbUserService;

        private List<Command> commands;
        private List<CallbackCommand> callbackCommands;

        public CommandService(DbActivitiesService dbActivitiesService,
            DbUserActionsService dbUserActionsService,
            DbUserService dbUserServices)
        {
            _dbActivitiesService = dbActivitiesService;
            _dbUserActionsService = dbUserActionsService;
            _dbUserService = dbUserServices;
            initCommands();
        }

        private void initCommands()
        {
            commands = new List<Command>();

            commands.Add(new StartCommand(_dbUserService));
            commands.Add(new ShowActivitiesMenuCommand(_dbUserService));
            commands.Add(new AddActivityCommand(_dbUserService, _dbActivitiesService));
            commands.Add(new ShowActionsMenuCommand(_dbUserService));
            commands.Add(new AddActionMainCommand(_dbUserService, _dbActivitiesService, _dbUserActionsService));
            commands.Add(new ShowMainKeyboardCommand(_dbUserService));

            callbackCommands = new List<CallbackCommand>();

            callbackCommands.Add(new AddActivityUtilityCommand(_dbUserService,_dbActivitiesService));
            callbackCommands.Add(new RemoveActivityCommand(_dbUserService,_dbActivitiesService));
            callbackCommands.Add(new RemoveActivityUtilityCommand(_dbUserService, _dbActivitiesService));
            callbackCommands.Add(new ShowActivitiesCommand(_dbActivitiesService, _dbUserService));
            callbackCommands.Add(new AddActionCommand(_dbUserService, _dbUserActionsService, _dbActivitiesService));
            callbackCommands.Add(new AddActionUtilityCommand(_dbActivitiesService, _dbUserService));
            callbackCommands.Add(new EndActionCommand(_dbUserService, _dbUserActionsService));
            callbackCommands.Add(new ShowActionsCommand(_dbUserService));
            callbackCommands.Add(new ShowActionsMainCommand(_dbUserService, _dbUserActionsService, _dbActivitiesService));
            callbackCommands.Add(new ShowActionsUtilityCommand(_dbUserService, _dbActivitiesService));
            callbackCommands.Add(new StartActionCommand(_dbUserService, _dbUserActionsService, _dbActivitiesService));
            callbackCommands.Add(new StartActionUtilityCommand(_dbUserService, _dbActivitiesService));
        }

        public IReadOnlyList<Command> GetCommands()
        {
            return commands.AsReadOnly();
        }

        public IReadOnlyList<CallbackCommand> GetCallbackCommands()
        {
            return callbackCommands.AsReadOnly();
        }
    }
}
