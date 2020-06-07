using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace FrontChatSystem.Function.Endpoints
{
    public static class GraphSubscription
    {
        [FunctionName("GraphSubscription")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [SignalR(HubName = "broadcast")]IAsyncCollector<SignalRMessage> signalRMessages
        )
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Graph Subscription Validation
            string token = req.Query["validationToken"];
            if (token != null)
            {
                log.LogInformation("token Validation");
                return new ContentResult { Content = token, ContentType = "text/plain", StatusCode = 200 };
            }

            // Subscription Action
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation(requestBody);

            await signalRMessages.AddAsync(new SignalRMessage()
            {
                Target = "notify",
                Arguments = new object[] { new { requestBody } }
            });

            return new OkObjectResult("run");
        }
    }
}
