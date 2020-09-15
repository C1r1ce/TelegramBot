using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramTimeManagementBot.Models
{
    public class UserAction
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
}
