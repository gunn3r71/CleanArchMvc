using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/account")]
    public class AccountController : BaseController
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public AccountController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _authentication.RegisterUserAsync(user.Email, user.Password);

            return Ok(success);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> LoginAsync([FromBody] LoginViewModel login)
        {
            if (!ModelState.IsValid) return BadRequest();

            var success = await _authentication.AuthenticateAsync(login.Email, login.Password);

            if (!success) BadRequest(ModelState);

            return Ok(GenerateToken(login));
        }

        private UserToken GenerateToken(LoginViewModel userData)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, userData.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenSettings:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _configuration["TokenSettings:Issuer"],
                audience: _configuration["TokenSettings:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        } 
    }
}
