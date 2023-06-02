using FluentValidation;
using TravelAgencyAPI.Entities;

namespace TravelAgencyAPI.Models.Validators
{
    public class TourDtoValidator : AbstractValidator<TourDto>
    {
        public TourDtoValidator()
        {
            RuleFor(tour => tour.StartDate)
                .LessThan(tour => tour.EndDate)
                .WithMessage("Start date must be less than end date")
                .NotEmpty()
                .WithMessage("Start date is required")
                .NotEqual(tour => tour.EndDate)
                .WithMessage("Start date cannot be equal to end date");
        }
    }
}
