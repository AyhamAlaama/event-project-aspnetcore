using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Event.Api.Extensions;
using Event.Application.Features.v1.Users.Command;
using Event.Domain.IdentityModels.ExtendedUser;


namespace Event.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Manage Users And Roles
        /// </summary>
        /// <param name="mediator"></param>
        public ManageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// register new user
        /// </summary>
        /// <param name="command">CreateUserCommand model</param>
        /// <returns> UserName , UserId</returns>
        [HttpPost("Users/SignUp")]
        public async Task<ActionResult<AuthModel>> CreateUser([FromBody]CreateUserCommand command)
        {
            var cmd = await _mediator.Send(command);
            //check if no error
            if (cmd.Message is null)
            {
                
                return Ok(cmd);
            }
            return Ok(cmd.Message);
        }
 
        /// <summary>
        /// Login user to system
        /// </summary>
        /// <param name="command"> UserName or Email , UserId</param>
        /// <returns>UserName , UserId</returns>
        [HttpPost("Users/Signin")]
        public async Task<ActionResult<AuthModel>> LoginUser([FromBody] LoginUserCommand command)
        {
            var cmd = await _mediator.Send(command);
            if (cmd.Message is null)
            {
                return Ok(new { token = cmd.Token });
            }
            return Ok(cmd.Message);
        }
   
        /// <summary>
        /// Get Logged UserInfo
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Users/UserId")]
        public  ActionResult<string> UserInfo()
         => Ok(HttpContext.GetUserId());


    }
}