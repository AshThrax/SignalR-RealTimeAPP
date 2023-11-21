using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using WebApplication1.interfaces.ChatClient;

namespace WebApplication1.SignalR
{
    public class ChatHub:Hub<IChatClient>
    {

        private bool IsUserAuthorized(string user)
        { 
            //perform authorization
            //return true if the user is authorized false otherwise

        }
        //in the chathub
        public override Task OnConnectedAsync()
        {
            //handle the deconnection
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        { 
            //handle deConnection to the hub
        }
        public async Task SendMessage(string user,string Message) 
        {
            if (IsUserAuthorized(user))
            {
                await Clients.All.SendAsync("receiveMessage", user, Message);
            }
            else
            { 
            
             }
        }
        public async Task JoinGroup(string  GroupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
        }
        public async Task SendMessageToGroup(string groupName, string user, string Message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, Message);
        }

    }
}
