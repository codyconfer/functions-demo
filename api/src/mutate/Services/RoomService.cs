using System;
using FunctionsDemo.Mutate.Models.Room;

namespace FunctionsDemo.Mutate.Services
{
    public class RoomService
    {
        public MessageAddResponse AddMessage(MessageAddRequest request)
        {
            return new MessageAddResponse 
            {
                MessageId = 0,
                Timestamp = DateTimeOffset.Now
            };
        }
    }
}