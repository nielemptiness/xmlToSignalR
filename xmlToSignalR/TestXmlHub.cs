using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace xmlToSignalR
{
    public interface ITestXmlHubClient
    {
        Task SendXml(string message);
    }

    public class TestXmlHub : Hub<ITestXmlHubClient>
    {
    }
}