using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Core.core
{
public interface ICustomWebSocketFactory
    {
        void Add(CustomWebSocket uws);
        void Remove(string username);
        List<CustomWebSocket> All();
    }   
     public class CustomWebSocketFactory : ICustomWebSocketFactory
    {
        List<CustomWebSocket> List;
        public CustomWebSocketFactory()
        {
            List = new List<CustomWebSocket>();
        }
        public void Add(CustomWebSocket uws)
        {
            if (List.Any(o => o.Username == uws.Username))
                Remove(uws.Username);
            List.Add(uws);
        }
        public void Remove(string username)
        {
            List.Remove(List.First(c => c.Username == username));
        }
        public List<CustomWebSocket> All()
        {
            return List;
        }
    }
}