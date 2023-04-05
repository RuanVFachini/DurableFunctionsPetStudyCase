using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using DurableFunctionsStudyCase.Functions.Consts;
using DurableFunctionsStudyCase.Domain.Pets;
using DurableFunctionsStudyCase.Application.Pets.Requests;

namespace DurableFunctionsStudyCase.Functions.Activities
{
    public class DogCreateExternalServiceActivity
    {
        [FunctionName(ActiviesConsts.Dog.Create)]
        public static Pet CreateDog([ActivityTrigger] IDurableActivityContext context)
        {
            //External Http Request

            return new Pet() { Name = context.GetInput<PetCreateRequest>().Name };
        }
    }
}
