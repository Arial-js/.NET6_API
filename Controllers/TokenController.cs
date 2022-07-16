using EsercitazioneAPI.Models;
using EsercitazioneAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Dotnet6_API.Models.DTO.User;

namespace EsercitazioneAPI.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordEncrypter _passwordEncrypter;

        public TokenController(IUserRepository userRepository, IConfiguration configuration, IPasswordEncrypter passwordEncrypter)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordEncrypter = passwordEncrypter;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> GetToken([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if(loginDTO is null)
                {
                    return BadRequest();
                }
                var user = await _userRepository.GetByEmail(loginDTO.Email);
                string salt = _passwordEncrypter.GetSalt();
                loginDTO.Password = _passwordEncrypter.ComputeSha512Hash(loginDTO.Password, salt);
                if(user.Password != loginDTO.Password)
                {
                    return Unauthorized();
                }
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                // Claims = https://auth0.com/docs/secure/tokens/json-web-tokens/json-web-token-claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Name + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                };
                //creazione Token
                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signinCredentials
                    );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = token});
            } 
            catch(AggregateException)
            {
                return NotFound("User not found");
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
