using Chat.Shared.Models.UserAndChatDTOS;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Hubs;

public class ChatHub : Hub
{
    private Dictionary<string, string> users = new();
    
    public override async Task OnConnectedAsync()
    {
        string username = Context.GetHttpContext().Request.Query["username"];
        users.Add(Context.ConnectionId, username);
        await base.OnConnectedAsync();
    }

    // public override async Task OnDisconnectedAsync(Exception? exception)
    // {
    //     string username = Context.GetHttpContext().Request.Query["username"];
    //     await AddMessage(String.Empty, $"{username} disconnected!");
    // }

    public async Task AddMessage(string user, MessageDTO message)
    {
        await Clients.All.SendAsync("ReceiveMessage",user, message);
    }

    public async Task ChangeMessage(MessageDTO message)
    {
        await Clients.All.SendAsync("ReceiveEditedMessage", message);
    }

    public async Task DeleteMessage(MessageDTO message)
    {
        await Clients.All.SendAsync("DeletedMessage", message);
    }
}