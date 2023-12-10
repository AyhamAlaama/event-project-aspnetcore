using MediatR;
using Event.Domain.IdentityModels.ExtendedUser;
using System.ComponentModel.DataAnnotations;

namespace Event.Application.Features.v1.Users.Command;

    public class CreateUserCommand :IRequest<AuthModel>
    {
        
        [Required,MinLength(3)]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }