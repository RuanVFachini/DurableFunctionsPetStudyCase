using DurableFunctionsStudyCase.Domain.Pets.Enums;
using System;
using System.Collections.Generic;

namespace DurableFunctionsStudyCase.Application.Schedules.Responses
{
    public class VaccinationResponse
    {
        public IList<VaccinationType> Vaccines { get; set; }
        public DateTime Time { get; set; }
    }
}
