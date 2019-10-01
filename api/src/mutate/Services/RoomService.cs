using System;
using FunctionsDemo.Mutate.Models.Room;
using StackExchange.Redis;

namespace FunctionsDemo.Mutate.Services
{
    public class RoomService
    {
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