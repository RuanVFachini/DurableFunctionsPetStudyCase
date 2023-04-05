using DurableFunctionsStudyCase.Application.Pets.Requests;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionsStudyCase.Application.Dogs.Validators
{
    public class PetFormValidator : AbstractValidator<PetCreateRequest>
    {
        public PetFormValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
