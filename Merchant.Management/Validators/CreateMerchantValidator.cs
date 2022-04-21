using FluentValidation;
using Merchant.Management.Models;

namespace Merchant.Management.Validators
{
    public class CreateMerchantValidator : AbstractValidator<CreateMerchant>
    {
        public CreateMerchantValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
        }
    }
}
