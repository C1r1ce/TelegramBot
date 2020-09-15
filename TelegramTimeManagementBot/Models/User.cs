using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramTimeManagementBot.Models
{
    public class User
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastMessageTime { get; set; }
        public string LastCommandQuery { get; set; }
    }
}
