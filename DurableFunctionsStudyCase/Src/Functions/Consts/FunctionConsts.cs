using DurableFunctionsStudyCase.Functions.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionsStudyCase.Functions.Consts
{
    public static class FunctionConsts
    {
        public static class Activities
        {
            public static class RetryPolicy
            {
                public const int DefaultMaxNumberOfAttempts = 3;
            }
        }

        public static class Schedules
        {
            public static class VeterinaryClinic
            {
                public const int DefaultOffsetInDays = 7;
            }
        }
    }
}
