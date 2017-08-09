namespace Landlords.Notifications
{
    using System;
    using System.Net.WebSockets;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Core;
    using Jwt;
    using Microsoft.IdentityModel.Tokens;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class WebSocketHandler
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public WebSocketHandler(WebSocketConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }

        protected WebSocketConnectionManager WebSocketConnectionManager { get; set; }

        public virtual async Task OnConnected(WebSocket socket, ClaimsPrincipal user)
        {
            WebSocketConnectionManager.AddSocket(socket, user);

            await SendMessageAsync(socket, new Message
            {
                MessageType = MessageType.ConnectionEvent,
                Data = WebSocketConnectionManager.GetId(socket)
            });
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
        }

        public async Task SendMessageAsync(WebSocket socket, Message message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            var serializedMessage = JsonConvert.SerializeObject(message, _jsonSerializerSettings);
            await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(serializedMessage), 0, serializedMessage.Length),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessageAsync(string socketId, Message message)
        {
            await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async Task InvokeClientMethodAsync(string socketId, string methodName, object[] arguments)
        {
            var message = new Message
            {
                MessageType = MessageType.ClientMethodInvocation,
                Data = JsonConvert.SerializeObject(new InvocationDescriptor
                {
                    MethodName = methodName,
                    Arguments = arguments
                }, _jsonSerializerSettings)
            };

            await SendMessageAsync(socketId, message);
        }

        public async Task ReceiveAsync(HttpContext context, WebSocket socket, WebSocketReceiveResult result, string serializedInvocationDescriptor)
        {
            var invocationDescriptor = JsonConvert.DeserializeObject<InvocationDescriptor>(serializedInvocationDescriptor);
            var method = GetType().GetMethod(invocationDescriptor.MethodName);

            if (method == null)
            {
                await SendMessageAsync(socket, new Message
                {
                    MessageType = MessageType.Text,
                    Data = $"Cannot find method {invocationDescriptor.MethodName}"
                });
                return;
            }

            var arguments = new List<object>()
            {
                context.Request.GetAccessToken()
            };
            arguments.AddRange(invocationDescriptor.Arguments);

            try
            {
                method.Invoke(this, arguments.ToArray());
            }
            catch (TargetParameterCountException)
            {
                await SendMessageAsync(socket, new Message
                {
                    MessageType = MessageType.Text,
                    Data = $"The {invocationDescriptor.MethodName} method does not take {invocationDescriptor.Arguments.Length} parameters!"
                });
            }
            catch (ArgumentException)
            {
                await SendMessageAsync(socket, new Message
                {
                    MessageType = MessageType.Text,
                    Data = $"The {invocationDescriptor.MethodName} method takes different arguments!"
                });
            }
            catch (Exception)
            {
                
            }
        }
    }
}