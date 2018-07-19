using System.Collections.Generic;
using FluentValidation;

namespace CitiesUsersApi.Models.Request
{
    public class UserRequestDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<int> CitiesIds { get; set; }
    }

    public class UserRequestDtoValidation : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name must be filled")
                .NotEmpty()
                .WithMessage("Name must be filled");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email must be wellformated")
                .NotNull()
                .WithMessage("Email must be filled")
                .NotEmpty()
                .WithMessage("Email must be filled");
        }
    }
}
