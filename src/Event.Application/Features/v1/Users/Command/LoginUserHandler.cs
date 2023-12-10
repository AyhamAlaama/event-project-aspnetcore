using MediatR;
using Event.Application.Interfaces;
using Event.Domain.IdentityModels.ExtendedUser;

namespace Event.Application.Features.v1.Users.Command;
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthModel>
    {
        private readonly IAuthService _authService;
        public LoginUserHandler(IAuthService authService)
        => _authService = authService;
        
        public async Task<AuthModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var authModel = await _authService.LoginAsync(request);
            return authModel;
        }
    }

