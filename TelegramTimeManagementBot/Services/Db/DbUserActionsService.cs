
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TelegramTimeManagementBot.Models;

namespace TelegramTimeManagementBot.Services.Db
{
    public class DbUserActionsService
    {
        private ApplicationContext _applicationContext;
        public DbUserActionsService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void AddAction(UserAction action)
        {
            _applicationContext.UserActions.Add(action);
            _applicationContext.SaveChanges();
        }

        public void UpdateAction(UserAction action)
        {
            _applicationContext.UserActions.Update(action);
            _applicationContext.SaveChanges();
        }

        public UserAction GetLastActiveActionByUserId(int userId)
        {
            var action = _applicationContext.UserActions.Where(ua => ua.UserId == userId)
                .Intersect(_applicationContext.UserActions.Where(ua => ua.TimeEnd == DateTime.MinValue)).AsNoTracking().FirstOrDefault();
            return action;
        }

        public TimeSpan GetAllTimeSpent(int activityId)
        {
            TimeSpan totalTime = new TimeSpan();
            var actions = _applicationContext.UserActions.Where(act => act.ActivityId == activityId).AsNoTracking().ToList();
            actions.ForEach(a => totalTime += a.TimeSpent);
            return totalTime;
        }

        public TimeSpan GetLastTimeSpent(int activityId, DateTime dateFrom)
        {
            TimeSpan time = new TimeSpan();
            var actions = _applicationContext.UserActions.Where(act => act.ActivityId == activityId)
                .Intersect(_applicationContext.UserActions.Where(act => act.TimeEnd > dateFrom)).AsNoTracking().ToList();

            actions.ForEach(act => time += act.TimeSpent);
            return time;
        }
    }
}
