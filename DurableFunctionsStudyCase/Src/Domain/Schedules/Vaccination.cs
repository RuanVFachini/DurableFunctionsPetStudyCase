using DurableFunctionsStudyCase.Domain.Pets;
using DurableFunctionsStudyCase.Domain.Pets.Enums;
using System;
using System.Collections.Generic;

namespace DurableFunctionsStudyCase.Domain.Schedules
{
    public class Vaccination
    {
        public Pet Pet { get; set; }
        public IList<VaccinationType> Vaccines { get; set; }
        public DateTime Time { get; set; }
    }
}
