using System.Collections.Generic;

namespace FunctionsDemo.Query.Models.Room
{
    public class MessagesResponse
    {
        public int RoomId {get; set;}
        public IEnumerable<Message> Messages { get; set; }
    }
}