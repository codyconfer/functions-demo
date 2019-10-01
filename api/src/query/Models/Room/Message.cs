using System;

namespace FunctionsDemo.Query.Models.Room
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
        public short ColorId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}