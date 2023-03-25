using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DurableFunctionsStudyCase.Application.Pets.Dogs;
using DurableFunctionsStudyCase.Functions.Consts;
using DurableFunctionsStudyCase.Functions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableFunctionsStudyCase.Functions
{
    public static class DogCreateFlowFunction
    {
        [FunctionName(FunctionsConsts.Dog.Create)]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var form = await req.GetJsonBody<DogRequest, DogFormValidator>();

            if (!form.IsValid)
            {
                log.LogInformation($"Invalid form data.");
                return form.ToBadRequest();
            }

            string instanceId = await starter.StartNewAsync(OrcherstratorsConsts.Dog.Create, null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}