namespace Business.Contracts;

public interface IClientService
{
    Task<string> ConnectToServer(string serverUrl);
    string SendMessageToServer();
}