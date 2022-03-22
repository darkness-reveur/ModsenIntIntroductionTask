using MeetupPlatform.Common.Helpers;
using MeetupPlatform.Common.Models.AuthenticationModels;
using MeetupPlatform.Infrastructure.Services.AuthenticationService;
using MeetupPlatform.Infrastructure.Services.Interfacies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VladosMeetupPlatform.API.Auth;

namespace VladosMeetupPlatform.API.Controllers.Authorize
{
    [Route("api/authorize/")]
    [ApiController]
    public class AuthorizeColtroller : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IUserService _userService;

        private readonly ILogger<AuthorizeColtroller> _logger;

        public AuthorizeColtroller(
            IAuthService authService,
            IUserService userService,
            ILogger<AuthorizeColtroller> logger)
        {
            _authService = authService;
            _logger = logger;
            _userService = userService;
        }

        private ClaimsIdentity Authenticate(string userId, string userRole, string userLogin)
        {
            var claims = new List<Claim>
            {
                new Claim(
                     ClaimsIdentity.DefaultNameClaimType,
                     userLogin),

                new Claim(
                    ClaimsIdentity.DefaultRoleClaimType,
                    userRole),

                 new Claim(
                    "userid",
                    userId),
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return identity;
        }

        [HttpPut]
        public async Task<IActionResult> LogInAsync([FromBody] LoginData data)
        {
            if (data.Login == null
                && data.Password == null)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthorizeColtroller),
                    nameof(LogInAsync),
                    $"Got an error. Login={data.Login} | Password={data.Password}",
                    new ArgumentNullException());

                return BadRequest();
            }

            var user = await _authService.LogIn(data);

            if (user is null)
            {
                _logger.LogWarning("Login data was incorrect");

                return NotFound();
            }

            var now = DateTime.UtcNow;

            var identity = Authenticate(
                 user.Id.ToString(),
                 user.Role.Name,
                 data.Login);

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
           
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            Response.Cookies.Append("Token", encodedJwt);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterData data)
        {
            if (data.Login == null
                || data.Password == null
                || data.User == null)
            {
                _logger.LogErrorByTemplate(
                    nameof(AuthorizeColtroller),
                    nameof(RegisterAsync),
                    $"Got an error. Login={!(data.Login is null)} " +
                        $"| Password={!(data.Password is null)} " +
                        $"| User={!(data.User is null)}",
                    new ArgumentNullException());

                return BadRequest();
            }

            if (await _authService.IsLoginFree(data.Login))
            {
                var user = await _userService
                    .AddUserAsync(data.User);

                data.User = user;

                await _authService.Register(data);

                return Ok(user);
            }

            return BadRequest();
        }
    }
}
