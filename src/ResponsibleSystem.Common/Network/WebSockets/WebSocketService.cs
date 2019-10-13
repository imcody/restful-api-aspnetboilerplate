using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ResponsibleSystem.Common.Logs;

namespace ResponsibleSystem.Common.Network.WebSockets
{
    public class WebSocketService : IWebSocketService
    {
        public async Task<string> ReciveWebSocketData(
            ClientWebSocket wsClient, 
            CancellationToken cancellationToken,
            ILogger logger = null)
        {
            ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[1024]);
            WebSocketReceiveResult wsResult = null;
            Byte[] dataBuffer;
            using (var ms = new MemoryStream())
            {
                do
                {
                    wsResult = await wsClient.ReceiveAsync(buffer, cancellationToken);
                    ms.Write(buffer.Array, buffer.Offset, wsResult.Count);

                    if (wsResult.CloseStatus != null)
                    {
                        logger?.Info(wsResult.CloseStatusDescription);
                    }

                } while (!wsResult.EndOfMessage);
                dataBuffer = ms.GetBuffer();
            }

            return wsResult.MessageType == WebSocketMessageType.Text
                ? Encoding.UTF8.GetString(dataBuffer, 0, dataBuffer.Length)
                : String.Empty;
        }
    }
}
