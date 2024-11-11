using System.Net;
using System.Net.NetworkInformation;
using Business.Contracts;
using Data.Contracts;
using Domain;
using FastSurvey.Controllers;

namespace Business.Implementation;

public class ServerConfigService(IServerConfigRepository serverConfigRepository, IRoleService roleService) : IServerConfigService
{
    public int Create(ServerConfig entity)
    {
        throw new NotImplementedException();
    }

    public int Read(int Id)
    {
        throw new NotImplementedException();
    }

    public int Update(ServerConfig entity, int Id)
    {
        throw new NotImplementedException();
    }

    public int Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> ScanLocalNetwork()
    {
        List<string> activeServers = new List<string>();
        string? localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList
            .FirstOrDefault( ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            ?.ToString();
        if (localIP == null)
            return activeServers;
        string subnet = string.Join(".", localIP.Split('.').Take(3)) + ".";
        Parallel.For(1, 255, i =>
        {
            string ip = subnet + i;
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = ping.Send(ip, 100);
                    if (reply.Status == IPStatus.Success)
                    {
                        lock (activeServers)
                        {
                            activeServers.Add(ip);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        });
        return activeServers;
    }

    public int CheckHealth()
    {
        if (roleService.Role == "server")
        {
            return 1;
        }

        if (roleService.Role == "client")
        {
            return 2;
        }

        return 0;
    }
}