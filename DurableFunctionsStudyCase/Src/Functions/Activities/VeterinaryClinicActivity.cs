using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using DurableFunctionsStudyCase.Functions.Consts;
using DurableFunctionsStudyCase.Domain.Pets;
using DurableFunctionsStudyCase.Domain.Schedules;
using DurableFunctionsStudyCase.Domain.Pets.Enums;

namespace DurableFunctionsStudyCase.Functions.Activities
{
    public class VeterinaryClinicActivity
    {
        [FunctionName(ActiviesConsts.VeterinaryClinic.ScheduleVaccination)]
        public static Vaccination ScheduleVaccination([ActivityTrigger] IDurableActivityContext context)
        {
            var pet = context.GetInput<Pet>();

            //External Http Request

            return new Vaccination()
            {
                Pet = pet,
                Vaccines = new List<VaccinationType>() { VaccinationType.Vaccine1, VaccinationType.Vaccine2 },
                Time = DateTime.UtcNow.AddDays(FunctionConsts.Schedules.VeterinaryClinic.DefaultOffsetInDays)
            };
        }

        [FunctionName(ActiviesConsts.VeterinaryClinic.ScheduleCheckUp)]
        public static CheckUp ScheduleCheckUp([ActivityTrigger] IDurableActivityContext context)
        {
            var pet = context.GetInput<Pet>();

            //External Http Request

            return new CheckUp()
            {
                Pet = pet,
                Time = DateTime.UtcNow.AddDays(FunctionConsts.Schedules.VeterinaryClinic.DefaultOffsetInDays)
            };
        }
    }
}
