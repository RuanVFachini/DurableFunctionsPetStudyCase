using DurableFunctionsStudyCase.Application.Pets.Requests;
using FluentValidation;

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
