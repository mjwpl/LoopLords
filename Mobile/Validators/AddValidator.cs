using FluentValidation;
using Mobile.Data.Models;

namespace Mobile.Validators
{
    public class AddValidator : AbstractValidator<Item>    {

        public AddValidator()
        {
            RuleFor(vm => vm.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
