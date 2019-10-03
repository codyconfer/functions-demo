using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunctionsDemo.Notify.Models.Room;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
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
        #endregion Redis

        public void StoreMessage(Message message)
        {
            var db = RedisConnection.GetDatabase();
            var messageKey = $"{MessageSetPrefix}0";
            var messageEntriesString = db.StringGet(messageKey);
            List<Message> messageEntries;
            if(!messageEntriesString.IsNullOrEmpty)
            {
                messageEntries = JsonConvert.DeserializeObject<List<Message>>(messageEntriesString);
                messageEntries.ToList().Add(message);
            }
            else
            {
                messageEntries = new List<Message>();
            }
            var messages = JsonConvert.SerializeObject(messageEntries);
            if(!db.StringSet(messageKey, messages, TimeSpan.FromHours(12)))
                throw new InvalidOperationException($"Error writing {message.Username} {message.Body} to redis");
        }
    }
}