using MediatR;
using Event.Application.Interfaces;
using Event.Domain.IdentityModels.ExtendedUser;

namespace Event.Application.Features.v1.Users.Command;

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, AuthModel>
    {
        private readonly IAuthService _authService;
        public CreateUserHandler(IAuthService authService)
        => _authService = authService;
        
        public async Task<AuthModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var authModel = await _authService.RegisterAsync(request);

            return authModel;
        }

 
    }
