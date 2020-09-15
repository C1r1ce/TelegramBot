using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramTimeManagementBot.Models;

namespace TelegramTimeManagementBot
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"");

        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
    }
}
