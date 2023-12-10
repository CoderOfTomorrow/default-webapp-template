using Default.WebApp.Template.Application.Commands.LoginUser;
using Default.WebApp.Template.Application.Commands.RegisterUser;
using Default.WebApp.Template.Application.Queries.GetUserDetails;
using Default.WebApp.Template.Server.Common.JwtToken;
using Default.WebApp.Template.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Default.WebApp.Template.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService jwtService;
        private readonly IMediator mediator;

        public AccountController(JwtService jwtService, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(jwtService);
            ArgumentNullException.ThrowIfNull(mediator);

            this.jwtService = jwtService;
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            var registerUseCommand = new RegisterUserCommand()
            {
                Username = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                Email = dto.Email
            };

            var result = await mediator.Send(registerUseCommand);

            return result.IsSuccessful ? Ok() : BadRequest(new { result.Error });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            var loginCommand = new LoginUserCommand()
            {
                Username = dto.Username,
                Password = dto.Password
            };

            var result = await mediator.Send(loginCommand);

            if (!result.IsSuccessful)
                return BadRequest(result.Error);

            var userDetailsQuery = new GetUserDetailsQuery()
            {
                Username = dto.Username
            };

            var userDetails = await mediator.Send(userDetailsQuery);

            string jwtToken = jwtService.CreateAuthToken(userDetails.Id, userDetails.Username, userDetails.Roles);

            Response.Cookies.Append("token", jwtToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.MaxValue
            });

            return Ok(jwtToken);
        }
    }
}