using System;
using System.Text;
using FunctionsDemo.Mutate.Models.Room;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace FunctionsDemo.Mutate.Services
{
    public class RoomService
    {
        #region EventHub
        private const string MessageHubName = "messages";

        private readonly string MessageHubConnectionString;

        public RoomService()
        {
            var hubConnectionSetting = Environment.GetEnvironmentVariable("EventHubConnection");
            if(string.IsNullOrEmpty(hubConnectionSetting))
            {
                throw new InvalidOperationException("EventHubConnection must be populated in app settings");
            }

            MessageHubConnectionString = 
                new EventHubsConnectionStringBuilder(hubConnectionSetting)
                {
                    EntityPath = MessageHubName
                }
                .ToString();
        }
        #endregion EventHub

        public MessageAddResponse AddMessage(MessageAddRequest request)
        {
            var client = EventHubClient.CreateFromConnectionString(MessageHubConnectionString);
            var json = JsonConvert.SerializeObject(request);
            var data = new EventData(Encoding.UTF8.GetBytes(json));
            client.SendAsync(data);
            return new MessageAddResponse
            {
                Timestamp = DateTimeOffset.Now
            };
        }
    }
}