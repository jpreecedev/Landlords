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
        private readonly ConcurrentDictionary<Guid, EndClient> _sockets = new ConcurrentDictionary<Guid, EndClient>();

        public ICollection<KeyValuePair<Guid, EndClient>> GetClientByUserId(Guid userId)
        {
            return _sockets.Where(p => p.Value.User.GetUserId() == userId).ToList();
        }

        public WebSocket GetSocketById(Guid id)
        {
            return _sockets.First(p => p.Key == id).Value.WebSocket;
        }

        public ConcurrentDictionary<Guid, EndClient> GetAll()
        {
            return _sockets;
        }

        public ICollection<Guid> GetConnectionIdsByUserId(Guid userId)
        {
            return _sockets.Where(c =>
            {
                var nameClaim = c.Value.User.FindFirst(ClaimTypes.NameIdentifier);
                var id = Guid.Parse(nameClaim.Value);
                return id == userId;
            })
            .Select(c => c.Key)
            .ToList();
        }

        public Guid GetId(WebSocket socket)
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

        public async Task RemoveSocket(Guid id)
        {
            _sockets.TryRemove(id, out EndClient endClient);

            await endClient.WebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                statusDescription: "Closed by the WebSocketManager",
                cancellationToken: CancellationToken.None);
        }

        private Guid CreateConnectionId()
        {
            return Guid.NewGuid();
        }
    }
}