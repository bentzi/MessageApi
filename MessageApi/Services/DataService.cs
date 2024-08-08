using System;
using System.Collections.Generic;
using System.Linq;
using MessageApi.Models;

namespace MessageApi.Services
{
    public class DataService
    {
        private List<User> users = new List<User>
        {
            new User { Id = "1234" },
            new User { Id = "5678" }
        };

        private List<Message> messages = new List<Message>
        {
            new Message { UserId = "1234", Text = "Hello, world!", Date = DateTime.Now },
            new Message { UserId = "5678", Text = "Hi there!", Date = DateTime.Now }
        };

        public bool CheckUser(string id)
        {
            return users.Any(u => u.Id == id);
        }

        public List<Message> GetMessages()
        {
            return messages;
        }

        public void AddMessage(Message message)
        {
            messages.Add(message);
        }
    }
}
