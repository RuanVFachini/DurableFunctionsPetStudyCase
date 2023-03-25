using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionsStudyCase.Application.Pets.Dogs
{
    public class DogFormValidator : AbstractValidator<DogRequest>
    {
        public DogFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
