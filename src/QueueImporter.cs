using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Occtoo.Onboarding.Sdk;
using Occtoo.Onboarding.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Occtoo.Provider.AzureQueueTrigger
{
    public class QueueImporter
    {
        private static readonly IOnboardingServiceClient _onboardingService = new OnboardingServiceClient(
            Environment.GetEnvironmentVariable("dataProviderId"),
            Environment.GetEnvironmentVariable("dataProviderSecret"));

        [FunctionName(nameof(QueueImporter))]
        public async Task Run([QueueTrigger("%nameOfYourQueue%")] string queueMsg, ILogger log)
        {
            var entities = new List<DynamicEntity>();
            var stockData = JsonSerializer.Deserialize<StockData>(queueMsg);
            entities.Add(new DynamicEntity
            {
                Key = stockData.Id,
                Properties = {
                    new DynamicProperty { Id = "materialNumber", Value = stockData.MaterialNumber },
                    new DynamicProperty { Id = "site", Value = stockData.Site },
                    new DynamicProperty { Id = "Stock", Value = stockData.Stock.ToString() },
                }
            });
            var response = await _onboardingService.StartEntityImportAsync(Environment.GetEnvironmentVariable("dataSource"), entities);
            if (response.StatusCode == 202)
            {
                // Data was onboarded!
            }
        }
    }
}
