using FluentValidation;

namespace WebAPICommonPitfalls.Common.Utilities
{   

    public class PaginationFilterValidator : AbstractValidator<PaginationFilter>
    {
        public PaginationFilterValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
                //.LessThanOrEqualTo(100).WithMessage("PageSize must be less than or equal to 100.");
        }
    }
}