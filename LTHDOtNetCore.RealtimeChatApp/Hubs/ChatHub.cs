using Microsoft.AspNetCore.SignalR;

namespace LTHDOtNetCore.RealtimeChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SeverReceiveMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientRecieveMessage", user, message);
        }
    }
}
