using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramTimeManagementBot.Models;

namespace TelegramTimeManagementBot.Services.Db
{
    public class DbActivitiesService
    {
        private ApplicationContext _applicationContext;
        public DbActivitiesService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void AddActivity(Activity activity)
        {
            if (!IsActivityExist(activity))
            {
                _applicationContext.Activities.Add(activity);
                _applicationContext.SaveChanges();
            }
        }

        public void UpdateActivity(Activity activity)
        {
            _applicationContext.Activities.Update(activity);
            _applicationContext.SaveChanges();
        }

        public void RemoveActivity(Activity activity)
        {
            _applicationContext.Activities.Remove(activity);
            _applicationContext.SaveChanges();
        }

        public Activity GetActivity(int userId, string name)
        {
            return _applicationContext.Activities.Where(a => a.UserId == userId)
                .Intersect(_applicationContext.Activities.Where(a => a.Name == name)).AsNoTracking().FirstOrDefault();
        }

        public Activity GetActivity(int activityId)
        {
            return _applicationContext.Activities.Find(activityId);
        }

        public List<Activity> GetActivities(int userId)
        {
            var activities = _applicationContext.Activities.Where(a => a.UserId == userId).AsNoTracking().ToList();
            return activities;
        }

        private bool IsActivityExist(Activity activity)
        {
            return _applicationContext.Activities.Where(a => a.Name == activity.Name)
                .Intersect(_applicationContext.Activities.Where(a => a.UserId == activity.UserId)).AsNoTracking().FirstOrDefault()!=null;
        }
    }
}
