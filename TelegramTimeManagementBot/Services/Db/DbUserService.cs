using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TelegramTimeManagementBot.Models;

namespace TelegramTimeManagementBot.Services.Db
{
    public class DbUserService
    {
        private ApplicationContext _applicationContext;
        public DbUserService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void AddUser(User user)
        {
            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _applicationContext.Users.Update(user);
            _applicationContext.SaveChanges();
        }

        public bool IsUserExist(User user)
        {
            var dbUser = _applicationContext.Users.Where(u=> u.ChatId == user.ChatId).AsNoTracking().FirstOrDefault();
            return dbUser != null? true : false;
        }

        public User GetUserByChatId(long chatId)
        {
            var user = _applicationContext.Users.Where(u => u.ChatId == chatId).FirstOrDefault();
            return user;
        }
    }
}
