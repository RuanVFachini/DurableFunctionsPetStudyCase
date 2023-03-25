using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DurableFunctionsStudyCase.Functions.Consts;

namespace DurableFunctionsStudyCase.Functions.Orcherstrators
{
    public class DogCreateFlowOrcherstator
    {
        [FunctionName(OrcherstratorsConsts.Dog.Create)]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            await context.CallActivityAsync<string>("Function1_Hello", "Tokyo");
        }
    }
}
