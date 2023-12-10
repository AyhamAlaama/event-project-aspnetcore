using MediatR;
using Event.Domain.IdentityModels.ExtendedUser;
using System.ComponentModel.DataAnnotations;

namespace Event.Application.Features.v1.Users.Command;

    public class LoginUserCommand:IRequest<AuthModel>
    {
        [Required]
        [Display(Name = "Email Or UserName")]
        public string? EmailOrUserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }

