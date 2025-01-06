using Microsoft.AspNetCore.SignalR;

namespace Service.UI.ChatHub
{
    public class chathubb : Hub
    {
        public async Task SendMessage(string Name, string UserType, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Name, UserType, message);

        }
    }
}
