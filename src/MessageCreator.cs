using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Occtoo.Provider.AzureQueueTrigger
{
    public static class MessageCreator
    {
        [FunctionName(nameof(MessageCreator))]
        public static async Task<IActionResult> Run(
            [Queue("%nameOfYourQueue%"), StorageAccount("AzureWebJobsStorage")] ICollector<string> queueMsg,
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var stockData1 = new StockData
            {
                Id = "123456",
                Site = "1255",
                MaterialNumber = "1177",
                Stock = 15
            };
            queueMsg.Add(JsonSerializer.Serialize(stockData1));

            var stockData2 = new StockData
            {
                Id = "456789",
                Site = "1256",
                MaterialNumber = "1337",
                Stock = 10
            };
            queueMsg.Add(JsonSerializer.Serialize(stockData2));

            var stockData3 = new StockData
            {
                Id = "789123",
                Site = "1257",
                MaterialNumber = "420",
                Stock = 2503
            };
            queueMsg.Add(JsonSerializer.Serialize(stockData3));

            return new OkObjectResult($"Added 3 messages to the queue");
        }
    }
}
