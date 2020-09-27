using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(Policy = "GameMaster")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ILog _log;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ILog log)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            _log = log;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                UserName = register.UserName,
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, register.Role);
                await _signInManager.SignInAsync(user, false);
                var token = CreateToken(user, new List<string>() { register.Role });
                await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);
                return Ok(new
                {
                    jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return BadRequest("Invalid registration");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);
                var identityRole = await _userManager.GetRolesAsync(user);
                var token = CreateToken(user, identityRole.ToList());

                return Ok(new
                {
                    jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Invalid login attempt");
        }

        private JwtSecurityToken CreateToken(ApplicationUser appUser, List<string> role)
        {
            // Token needs info called "Claims" which tell something True abou the user. I.e. I have red hair, my name is Nicholas, etc..
            // A User is the "principle" and can have many forms of "identity" containing these claims i.e. birth cert, drivers license, SSN, etc...
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                // Optional:
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserName", appUser.UserName),
                new Claim("UserId", appUser.Id),
            };

            foreach (var item in role)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            var token = AuthenticateToken(authClaims);
            return token;
        }

        private JwtSecurityToken AuthenticateToken(List<Claim> claims)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKey"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWTIssuer"],
                expires: DateTime.UtcNow.AddHours(24),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}