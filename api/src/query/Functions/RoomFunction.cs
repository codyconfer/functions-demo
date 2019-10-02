using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionsDemo.Query.Services.Room;
using Newtonsoft.Json.Serialization;

namespace FunctionsDemo.Query.Functions
{
    public static class RoomFunction
    {
        [FunctionName(nameof(GetMessages))]
        public static async Task<IActionResult> GetMessages(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "room/messages")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string query = req.Query["roomId"];

            if(!int.TryParse(query, out int roomId))
            {
                return new BadRequestObjectResult("Could not parse roomId");
            }
            
            var service = new RoomService();
            var result = service.GetMessages(roomId);
            var response = JsonConvert.SerializeObject(result, SerializerSettings);

            return new OkObjectResult(response);
        }

        public static JsonSerializerSettings SerializerSettings =>
            new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
            };
    }
}
