using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DurableFunctionsStudyCase.Functions.Consts;
using DurableFunctionsStudyCase.Domain.Pets;
using DurableFunctionsStudyCase.Domain.Schedules;
using DurableTask.Core.Exceptions;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MapsterMapper;
using DurableFunctionsStudyCase.Application.Pets.Requests;
using DurableFunctionsStudyCase.Application.Pets.Responses;

namespace DurableFunctionsStudyCase.Functions.Orcherstrators
{
    public class DogCreateFlowOrcherstator
    {
        private readonly IMapper _mapper;

        public DogCreateFlowOrcherstator(IMapper mapper)
        {
            _mapper = mapper;
        }

        [FunctionName(OrcherstratorsConsts.Dog.Create)]
        public async Task<PetCreateResponse> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger log)
        {
            var retryOptions = GetRetryOptions();
            var input = context.GetInput<PetCreateRequest>();

            log.LogInformation("Started orcherstrator {0}. Payload: {1}", new[] { context.InstanceId, JsonConvert.SerializeObject(input)});

            try
            {
                var dog = await context.CallActivityWithRetryAsync<Pet>(ActiviesConsts.Dog.Create, retryOptions, input);
                dog.CheckUps.Add(await context.CallActivityWithRetryAsync<CheckUp>(ActiviesConsts.VeterinaryClinic.ScheduleCheckUp, retryOptions, dog));
                dog.Vaccinations.Add(await context.CallActivityWithRetryAsync<Vaccination>(ActiviesConsts.VeterinaryClinic.ScheduleVaccination, retryOptions, dog));
                log.LogInformation("Finished orcherstrator {0}. Result: {1}", new[] { context.InstanceId, JsonConvert.SerializeObject(dog)});
                return _mapper.Map<PetCreateResponse>(dog);
            } catch (TaskFailedException ex)
            {
                log.LogCritical(ex, "Finished orcherstrator {0} with errors. Details: {1}", new[]{ context.InstanceId, ex.Message});
                throw; 
            }
        }

        private static RetryOptions GetRetryOptions()
        {
            var codesToRetry = new List<HttpStatusCode?>()
            {
                HttpStatusCode.InternalServerError,
                HttpStatusCode.BadGateway
            };

            return new RetryOptions(
                            firstRetryInterval: TimeSpan.FromSeconds(5),
                            maxNumberOfAttempts: FunctionConsts.Activities.RetryPolicy.DefaultMaxNumberOfAttempts)
            {
                Handle = exception =>
                {
                    if ((exception is TaskFailedException failure) && (exception is HttpRequestException requestException))
                    {
                        return codesToRetry.Contains(requestException.StatusCode);
                    }

                    return false;
                }
            };
        }
    }
}
