using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Logs;

namespace ResponsibleSystem.Common.Network.WebSockets
{
    public interface IWebSocketService
    {
        Task<string> ReciveWebSocketData(ClientWebSocket wsClient, CancellationToken cancellationToken, ILogger logger = null);
    }
}
