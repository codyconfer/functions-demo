using FunctionsDemo.Query.Models.Room;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace FunctionsDemo.Query.Services.Room
{
    public class RoomService
    {

        #region Redis
        private const string MessageSetPrefix = "messages-";

        private readonly Lazy<ConnectionMultiplexer> _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var connectionString = Environment.GetEnvironmentVariable("RedisConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("RedisConnection must be populated in app settings");
            }

            return ConnectionMultiplexer.Connect(connectionString);
        });

        private ConnectionMultiplexer RedisConnection => _lazyConnection.Value;
        #endregion Redis

        public MessagesResponse GetMessages(int roomId)
        {
            var db = RedisConnection.GetDatabase();
            var messageKey = $"{MessageSetPrefix}{roomId}";
            var entry = db.StringGet(messageKey);
            if (entry.IsNullOrEmpty)
                return new MessagesResponse
                {
                    Messages = new List<Message>(),
                    RoomId = 0
                }; ;
            var messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(entry);
            return new MessagesResponse
            {
                Messages = messages,
                RoomId = 0
            };
        }

        public void InflateMockMessages()
        {
            var db = RedisConnection.GetDatabase();
            var messageKey = $"{MessageSetPrefix}0";
            var messages = JsonConvert.SerializeObject(MockMessages);
            db.StringSet(messageKey, messages, TimeSpan.FromMinutes(10));
        }

        public IEnumerable<Message> MockMessages =>
            new List<Message> {
                new Message
                {
                    MessageId = 0,
                    Username = "Username",
                    Body = "Hello everyone",
                    ColorId = 0,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 1,
                    Username = "User3455",
                    Body = "Hello",
                    ColorId = 1,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 2,
                    Username = "User64545",
                    Body = "How is it going?",
                    ColorId = 2,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 3,
                    Username = "Bob",
                    Body = "Pretty well",
                    ColorId = 3,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 4,
                    Username = "Tim",
                    Body = "alright here",
                    ColorId = 4,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 5,
                    Username = "User9203",
                    Body = "I'm here now",
                    ColorId = 5,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 6,
                    Username = "User4272724",
                    Body = "No one cares",
                    ColorId = 6,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 7,
                    Username = "User937357",
                    Body = "Sure",
                    ColorId = 7,
                    Timestamp = DateTimeOffset.Now
                },
                new Message
                {
                    MessageId = 8,
                    Username = "User123",
                    Body = "Leaving...",
                    ColorId = 8,
                    Timestamp = DateTimeOffset.Now
                }
            };
    }
}