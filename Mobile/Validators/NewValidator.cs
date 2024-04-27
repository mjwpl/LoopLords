using FluentValidation;
using Mobile.ViewModel;

namespace Mobile.Validators
{
    public class NewValidator : AbstractValidator<NewViewModel>
    {
        public NewValidator()
        {
            RuleFor(vm => vm.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(vm => vm.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(vm => vm.LoopInDays).GreaterThan(0).WithMessage("Loop in days must be greater than 0");
        }
    }
}
