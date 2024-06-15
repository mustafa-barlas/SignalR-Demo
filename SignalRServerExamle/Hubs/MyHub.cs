using Microsoft.AspNetCore.SignalR;
using SignalRServerExamle.Interfaces;

namespace SignalRServerExamle.Hubs;

public class MyHub : Hub<IMessageClient>
{
    private static List<string> _clients = new List<string>();

    // public async Task SendMessageAsync(string message)
    // {
    //     await Clients.All.SendAsync("receiveMessage", message);
    // }

    public override async Task OnConnectedAsync()
    {
        _clients.Add(Context.ConnectionId);
        // await Clients.All.SendAsync("clients", _clients);
        // await Clients.All.SendAsync("userJoined", Context.ConnectionId);

        await Clients.All.Clients(_clients);
        await Clients.All.UserJoined(Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _clients.Remove(Context.ConnectionId);
        // await Clients.All.SendAsync("clients", _clients);
        // await Clients.All.SendAsync("userLeft", Context.ConnectionId);

        await Clients.All.Clients(_clients);
        await Clients.All.UserLeft(Context.ConnectionId);
    }
}