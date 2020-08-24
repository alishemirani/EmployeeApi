using System.Threading.Tasks;
using EmployeeApi.DTO;
using EmployeeApi.Models;
using EmployeeApi.Services;
using EmployeeApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmployeeApi.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(
            IConfiguration configuration,
            IUserService userService,
            ILogger<AuthenticationController> logger)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.logger = logger;
        }

        /// <summary>
        /// Attmept to obtain valid JWT token by providing a valid user credentials
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody]LoginModelDTO loginModel) {
            
            var user = await userService.AuthenticateUser(loginModel.Username, loginModel.Password);
            if (user == null) {
                return Unauthorized();
            }
            var tokenConfig = configuration.GetSection("Jwt").Get<TokenConfig>();
            string token = JwtTokenProvider.GenerateToken(tokenConfig, user);
            return Ok(new {
                token = token
            });
        }
    }
}