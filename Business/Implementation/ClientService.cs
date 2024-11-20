using Business.Contracts;
using Microsoft.AspNetCore.SignalR.Client;

namespace Business.Implementation;

public class ClientService : IClientService
{
    private HubConnection? _hubConnection;

    public async Task<string> ConnectToServer(string serverUrl)
    {
        if (_hubConnection != null && _hubConnection.State == HubConnectionState.Connected)
            return "already connected";

        try
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{serverUrl}/centralHub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("ReceiveMessage", message =>
            {
                Console.WriteLine($"Message from server: {message}");
            });

            await _hubConnection.StartAsync();
            return "connected";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to server: {ex.Message}");
            return $"error: {ex.Message}";
        }
    }

    public string SendMessageToServer()
    {
        if (_hubConnection == null || _hubConnection.State != HubConnectionState.Connected)
        {
            return "not connected";
        }

        _hubConnection.SendAsync("SendMessage");
        return "message sent";
    }
}
