using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionsStudyCase.Functions.Consts
{
    public static class ActiviesConsts
    {
        public static class Dog
        {
            public const string Create = "DurableFunctionsStudyCase_Functions_Activity_Dog_Create";
        }

        public static class VeterinaryClinic
        {
            public const string ScheduleCheckUp = "DurableFunctionsStudyCase_Functions_Activity_VeterinaryClinic_Schedule_CheckUp";
            public const string ScheduleVaccination = "DurableFunctionsStudyCase_Functions_Activity_VeterinaryClinic_Schedule_Vaccination";
        }
    }
}
