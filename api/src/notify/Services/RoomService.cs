using System;
using FunctionsDemo.Notify.Models.Room;
using StackExchange.Redis;

namespace FunctionsDemo.Notify.Services
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

        private readonly IDatabase _db;
        #endregion Redis


    }
}