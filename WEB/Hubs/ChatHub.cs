using DOMAIN.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace WEB.Hubs
{
    public class ChatHub : Hub
    {
        protected readonly IServiceProvider _serviceProvider;

        protected static readonly Dictionary<int, string> _connectedIds = new Dictionary<int, string>();

        public ChatHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task OnConnectedAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var userName = Context.User.Identity.Name;             
                var userId = userManager.FindByNameAsync(userName).Result.Id;

                if (!_connectedIds.ContainsKey(userId))
                {
                    _connectedIds.Add(userId, Context.ConnectionId);
                }
                else {
                    _connectedIds[userId] = Context.ConnectionId;
                }

                Clients.All.SendAsync("online", userId, JsonConvert.SerializeObject(_connectedIds));
            }
                
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var userName = Context.User.Identity.Name;
                var userId = userManager.FindByNameAsync(userName).Result.Id;

                if (_connectedIds.ContainsKey(userId))
                {
                    _connectedIds.Remove(userId);
                }

                Clients.All.SendAsync("offline", userId, JsonConvert.SerializeObject(_connectedIds));
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string receiverId, string message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var userName = Context.User.Identity.Name;
                var userId = userManager.FindByNameAsync(userName).Result.Id;
                await Clients.User(receiverId).SendAsync("receiveMessage", userId, message);
            } 
        }

    }
}
