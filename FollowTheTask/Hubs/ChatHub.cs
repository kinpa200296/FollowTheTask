using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.Models.Chat;
using Microsoft.AspNet.SignalR;

namespace FollowTheTask.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();
        static List<UserMessage> Messages = new List<UserMessage>(); 

        public void Send(string name, string message)
        {
            Messages.Add(new UserMessage{UserName = name, Message = message});
            Clients.All.addMessage(name, message);
        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (Users.All(x => x.ConectionId != id))
            {
                Users.Add(new User{ConectionId = id, Name = userName});

                Clients.Caller.onConnected(id, userName, Users, Messages);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}