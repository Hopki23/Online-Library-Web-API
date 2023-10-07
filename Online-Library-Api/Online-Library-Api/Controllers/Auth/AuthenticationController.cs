namespace Online_Library_Api.Controllers.Auth
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    using Online_Library_Api.Data.Entities.User;
    using Online_Library_Api.DTOs.Auth;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AuthenticationController
            (UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            //Chech dto attributes
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await this.userManager.FindByEmailAsync(registerUser.Email);

            //check if already exist
            if (user != null)
            {
                return BadRequest("User with this email already exist!");
            }

            var newUser = new ApplicationUser()
            {
                Email = registerUser.Email,
                UserName = registerUser.UserName,
                Books = new HashSet<Data.Entities.Book>()
            };

            var result = await this.userManager.CreateAsync(newUser, registerUser.Password);

            //Check if user creation is succeeded
            if (result.Succeeded)
            {
                //Generate token
               var token = GenerateJwtToken(newUser);

                return Ok(token);
            }

            return BadRequest(result.Errors);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid credentials");
            }

            var user = await this.userManager.FindByEmailAsync(loginUser.Email);

            if (user == null)
            {
                return BadRequest("Invalid credentials");
            }

            var result = await this.userManager.CheckPasswordAsync(user, loginUser.Password);

            if (!result)
            {
                return BadRequest("Invalid credential");
            }

            var jwtToken = GenerateJwtToken(user);

            return Ok(new { Token = jwtToken, Id = user.Id, Username = user.UserName, Email = user.Email});
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(this.config.GetValue<string>("JwtConfig:Key"));
            var issuer = this.config.GetValue<string>("JwtConfig:Issuer");
            var audience = this.config.GetValue<string>("JwtConfig:Audience");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    //new Claim("Id", user.Id),
                    //new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    //new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
