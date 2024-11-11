using Domain;

namespace Business.Contracts;

public interface IServerConfigService : IGenericService<ServerConfig>
{
    IEnumerable<string> ScanLocalNetwork();
    int CheckHealth();
}