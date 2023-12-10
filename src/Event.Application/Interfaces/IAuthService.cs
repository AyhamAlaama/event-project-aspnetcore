
using Event.Application.Features.v1.Users.Command;
using Event.Domain.IdentityModels.ExtendedUser;
using System.Security.Claims;

namespace Event.Application.Interfaces;



public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(CreateUserCommand model);
        Task<AuthModel> LoginAsync(LoginUserCommand model);


    }

