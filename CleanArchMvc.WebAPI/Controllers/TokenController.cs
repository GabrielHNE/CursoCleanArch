using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.ResgisterUser(userInfo.Email, userInfo.Password);

            if(!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid register attemption");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

            if(result)
            {
                return GenerateToken(userInfo);
            }

            ModelState.AddModelError(string.Empty, "Invalid login Attempt");        
            return BadRequest(ModelState);
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("Email", userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Gerar chave privada
            var privateKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // Gerar assinatura digital
            var credentials = new  SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            // Definir tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(10);

            // Gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}