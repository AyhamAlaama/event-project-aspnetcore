using Microsoft.Extensions.Options;
using Event.Application.Features.v1.Users.Command;
using Event.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Event.Implementation.ImplementRepositories
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _role;
        private readonly JWT _jwt;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ApplicationDbContext _context;

        public AuthService(UserManager<ApplicationUser> userManager,
                           IOptions<JWT> jwt, RoleManager<IdentityRole> role,
                           TokenValidationParameters tokenValidationParameters,
                           ApplicationDbContext context)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _role = role;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
        }
        public async Task<AuthModel> LoginAsync(LoginUserCommand model)
        {
            var authModel = new AuthModel();
            var user = new ApplicationUser();
            if (model.EmailOrUserName.Contains("@"))
                user = await _userManager.FindByEmailAsync(model.EmailOrUserName);
            else
                user = await _userManager.FindByNameAsync(model.EmailOrUserName);
            if (user is null || !await _userManager.
                CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Invalid Credentials";
                return authModel;
            }
            var jwtModel = await CreateJwtTokenAsync(user);
            

          
            authModel.UserId = user.Id;
            authModel.IsAuthenticated = true;
            authModel.Token = jwtModel.Token;
            authModel.UserName = user.UserName;
            authModel.Email = user.Email;

            return authModel;

        }
        public async Task<AuthModel> RegisterAsync(CreateUserCommand model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "UserName is already registered!" };
            var user = new ApplicationUser
            {
                
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var err in result.Errors)
                    errors += $"{err.Description},";
                return  new AuthModel {Message=errors} ;


            }
            await _userManager.AddToRoleAsync(user, "User");

            var jwtModel = await CreateJwtTokenAsync(user);

            return new AuthModel
            {
                UserId = user.Id,
                IsAuthenticated = true,
                Token = jwtModel.Token ,
                UserName = user.UserName,
                Email=user.Email,

            };
        
        }
        private async Task<AuthModel> CreateJwtTokenAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles",role));
            }
            var claims = new ClaimsIdentity(claims:new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)

            }
            .Union(roleClaims));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials
                (symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {   
                Issuer = _jwt.Issuer,
                Audience =_jwt.Audience,
                Subject = claims,
                Expires = DateTime.UtcNow.Add(_jwt.Expires),
                SigningCredentials = signingCredentials,};
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new AuthModel 
            { Token=token,IsAuthenticated=true};
        }
    }

}
