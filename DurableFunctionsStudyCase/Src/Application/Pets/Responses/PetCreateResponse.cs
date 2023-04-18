using DurableFunctionsStudyCase.Application.Schedules.Responses;
using System.Collections.Generic;

namespace DurableFunctionsStudyCase.Application.Pets.Responses
{
    public class PetCreateResponse
    {
        public string Name { get; set; }
        public IList<CheckUpResponse> CheckUps { get; set; }
        public IList<VaccinationResponse> Vaccinations { get; set; }
    }
}
