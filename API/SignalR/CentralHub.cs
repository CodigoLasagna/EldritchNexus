using Microsoft.AspNetCore.SignalR;

namespace EldritchNexus.SignalR;

public class CentralHub : Hub
{
    private static List<string> _connectedClients = new List<string>();
    private static List<string> _knownClients = new List<string>();
    
    public async Task SendMessage()
    {
        await Clients.All.SendAsync("ReceiveMessage");
    }
    
    //on client connection
    public override async Task OnConnectedAsync()
    {
        _connectedClients.Add(Context.ConnectionId);
        await Clients.All.SendAsync("UpdatedClientCount", _connectedClients.Count);
        await base.OnConnectedAsync();
    }

    //on client disconnection
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _connectedClients.Remove(Context.ConnectionId);
        await Clients.All.SendAsync("UpdatedClientCount", _connectedClients.Count);
        await base.OnDisconnectedAsync(exception);
    }

    public List<string> KnownClients()
    {
        return _connectedClients;
    }
}