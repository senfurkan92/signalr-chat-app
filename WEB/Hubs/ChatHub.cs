using DOMAIN.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace WEB.Hubs
{
    public class ChatHub : Hub
    {
        protected readonly IServiceProvider _serviceProvider;

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
                Clients.All.SendAsync("online", userId);
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
                Clients.All.SendAsync("offline", userId);
            }

            return base.OnDisconnectedAsync(exception);
        }


    }
}
