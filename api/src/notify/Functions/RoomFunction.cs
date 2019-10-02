using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionsDemo.Notify.Models.Room;
using FunctionsDemo.Notify.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;

namespace FunctionsDemo.Notify.Functions
{
    public static class RoomFunction
    {
        [FunctionName("negotiate")]
        public static IActionResult GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "messages")] SignalRConnectionInfo connectionInfo)
        {
            return (ActionResult)new OkObjectResult(connectionInfo);
        }

        [FunctionName(nameof(MessagesObserver))]
        public static async Task MessagesObserver(
            [EventHubTrigger("messages",
                Connection = "EventHubConnection")] 
            EventData[] events,
            [SignalR(HubName = "messages")] IAsyncCollector<SignalRMessage> signalR,
            ILogger log)
        {
            var exceptions = new List<Exception>();
            var service = new RoomService();
            foreach (var eventData in events)
            {
                try
                {
                    var body = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    var message = JsonConvert.DeserializeObject<Message>(body);
                    log.LogInformation(message.Body);
                    await signalR.AddAsync(
                        new SignalRMessage
                        {
                            Target = "addMessage",
                            Arguments = new object[] { message }
                        });
                    service.StoreMessage(message);
                    await Task.Yield();
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
