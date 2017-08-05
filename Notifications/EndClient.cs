namespace Landlords.Notifications
{
    using System.Net.WebSockets;
    using System.Security.Claims;

    public class EndClient
    {
        public WebSocket WebSocket { get; set; }

        public ClaimsPrincipal User { get; set; }
    }
}