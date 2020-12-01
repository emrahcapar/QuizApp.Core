using System.Net.WebSockets;

namespace QuizApp.Core.core
{
    public class CustomWebSocket
    {
        public CustomWebSocket(WebSocket webSocket, string userName)
        {
            this.WebSocket = webSocket;
            this.Username = userName;
        }

        public WebSocket WebSocket { get; }
        public string Username{ get; }
    }
}