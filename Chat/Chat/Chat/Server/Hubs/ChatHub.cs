using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Hubs;

public class ChatHub : Hub
{
    private Dictionary<string, string> users = new();
    public override async Task OnConnectedAsync()
    {
        string username = Context.GetHttpContext().Request.Query["username"];
        users.Add(Context.ConnectionId, username);
        await AddMessage("", $"{username} connected!"); 
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string username = Context.GetHttpContext().Request.Query["username"];;
        await AddMessage(String.Empty, $"{username} disconnected!");
    }

    public async Task AddMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    
}