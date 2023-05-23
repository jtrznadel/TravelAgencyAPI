using FluentValidation;
using TravelAgencyAPI.Entities;

namespace TravelAgencyAPI.Models.Validators
{
    public class TourQueryValidator : AbstractValidator<TourQuery>
    {
        private int[] allowedPageSizes = { 5, 10, 15 };

        private string[] allowedSortByColumnNames = { nameof(Tour.Name), nameof(Tour.Country), nameof(Tour.StartDate) };
        public TourQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrWhiteSpace(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional or must be in [{string.Join(",",allowedSortByColumnNames)}]");
        }
    }
}
