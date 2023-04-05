using DurableFunctionsStudyCase.Domain.Pets.Enums;
using DurableFunctionsStudyCase.Domain.Schedules;
using System.Collections.Generic;

namespace DurableFunctionsStudyCase.Domain.Pets
{
    public class Pet
    {
        public string Name { get; set; }
        public static PetType Type { get => PetType.Dog; }
        public IList<CheckUp> CheckUps { get; set; } = new List<CheckUp>();
        public IList<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
    }
}
