using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionsDemo.Mutate.Services;
using FunctionsDemo.Mutate.Models.Room;

namespace FunctionsDemo.Mutate.Functions
{
    public static class RoomFunction
    {
        [FunctionName(nameof(AddMessage))]
        public static async Task<IActionResult> AddMessage(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "room/message")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var request = JsonConvert.DeserializeObject<MessageAddRequest>(await req.ReadAsStringAsync());

            if(request == null)
            {
                return new BadRequestObjectResult("Could not parse request.");
            }

            var service = new RoomService();
            var result = service.AddMessage(request);
            var response = JsonConvert.SerializeObject(result);

            return new OkObjectResult(response);
        }
    }
}
