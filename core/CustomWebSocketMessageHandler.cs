using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuizApp.Core.core
{
    public interface ICustomWebSocketMessageHandler
    {
        Task HandleMessage(byte[] buffer, ICustomWebSocketFactory wsFactory);
        Task HandleNewUserMessage(ICustomWebSocketFactory wsFactory);
    }

    public class CustomWebSocketMessageHandler : ICustomWebSocketMessageHandler
    { 
        public NewQuestionSocketMessage LastQuestionSocketMessage { get; set; }
        NewQuestionSocketMessage newQuestionSocketMessage;
        public async Task HandleMessage(byte[] buffer, ICustomWebSocketFactory wsFactory)
        {
            string msg = Encoding.ASCII.GetString(buffer);
            var message = JsonConvert.DeserializeObject<UserAnswerQuestionSocketMessage>(msg);
            var all = wsFactory.All(); 
            newQuestionSocketMessage = new NewQuestionSocketMessage
            {
                NewQuestion = QuestionService.GetRandomQuestion(),
                LastQuestionIsAnswerCorrect = message.IsAnswerCorrect,
                NewQuestionDate = DateTime.Now.AddSeconds(5),
                Username = message.Username
            }; 
            string serialisedMessage = JsonConvert.SerializeObject(newQuestionSocketMessage);
            byte[] bytes = Encoding.UTF8.GetBytes(serialisedMessage);
            foreach (var uws in all)
            {
                await uws.WebSocket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task HandleNewUserMessage(ICustomWebSocketFactory wsFactory)
        {
            var all = wsFactory.All();
            if (all.Count > 1)
            {
                var newMsg = new NewQuestionSocketMessage
                {
                    NewQuestion = QuestionService.GetRandomQuestion(),
                    NewQuestionDate = DateTime.Now,

                };
                string serialisedMessage = JsonConvert.SerializeObject(newMsg);
                byte[] bytes = Encoding.UTF8.GetBytes(serialisedMessage);
                foreach (var uws in all)
                {
                    await uws.WebSocket.SendAsync(new ArraySegment<byte>(bytes,
                     0, bytes.Length), WebSocketMessageType.Text, true,
                      CancellationToken.None);
                }
            }
        }
    }
}