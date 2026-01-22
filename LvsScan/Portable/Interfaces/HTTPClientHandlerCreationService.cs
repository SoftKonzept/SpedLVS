using System.Net.Http;

namespace LvsScan.Portable.Interfaces
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
