namespace Landlords.Notifications
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;

    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, EndClient> _sockets = new ConcurrentDictionary<string, EndClient>();

        public ICollection<KeyValuePair<string, EndClient>> GetClientByUserId(Guid userId)
        {
            return _sockets.Where(p => p.Value.User.GetUserId() == userId).ToList();
        }

        public WebSocket GetSocketById(string id)
        {
            return _sockets.First(p => p.Key == id).Value.WebSocket;
        }

        public ConcurrentDictionary<string, EndClient> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value.WebSocket == socket).Key;
        }

        public void AddSocket(WebSocket socket, ClaimsPrincipal user)
        {
            _sockets.TryAdd(CreateConnectionId(), new EndClient
            {
                WebSocket = socket,
                User = user
            });
        }

        public async Task RemoveSocket(string id)
        {
            _sockets.TryRemove(id, out EndClient endClient);

            await endClient.WebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                statusDescription: "Closed by the WebSocketManager",
                cancellationToken: CancellationToken.None).ConfigureAwait(false);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}