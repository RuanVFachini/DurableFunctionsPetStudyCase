using DurableFunctionsStudyCase.Domain.Pets;
using System;

namespace DurableFunctionsStudyCase.Domain.Schedules
{
    public class CheckUp
    {
        public Pet Pet { get; set; }
        public DateTime Time { get; set; }
    }
}
