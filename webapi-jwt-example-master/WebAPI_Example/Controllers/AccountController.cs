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
using WebAPI_Example.Models;

namespace WebAPI_Example.Controllers
{
    [Route("api/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] User model)
        {
            if (model == null)
            {
                return Json(new
                {
                    Succeeded = false,
                    Errors = new dynamic[] {
                        new {
                            Code = "NullReferenceException",
                            Description = "Parameters are null"
                        }
                    }
                });
            }
            else if (string.IsNullOrEmpty(model.Email))
            {
                return Json(new
                {
                    Succeeded = false,
                    Errors = new dynamic[] {
                        new {
                            Code = "NullReferenceException",
                            Description = "Email is null or empty"
                        }
                    }
                });
            }
            else if (string.IsNullOrEmpty(model.Password))
            {
                return Json(new
                {
                    Succeeded = false,
                    Errors = new dynamic[] {
                        new {
                            Code = "NullReferenceException",
                            Description = "Password is null or empty"
                        }
                    }
                });
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return Json(new
                {
                    Succeeded = true,
                    Token = GenerateJwtToken(model.Email, appUser)
                });
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Register([FromBody] User model)
        {
            if (model == null)
            {
                return Json(new
                {
                    Succeeded = false,
                    Errors = new dynamic[] {
                        new {
                            Code = "NullReferenceException",
                            Description = "Parameters are null"
                        }
                    }
                });
            }
            else if (string.IsNullOrEmpty(model.Email))
            {
                return Json(new
                {
                    Succeeded = false,
                    Errors = new dynamic[] {
                        new {
                            Code = "NullReferenceException",
                            Description = "Email is null or empty"
                        }
                    }
                });
            }
            else if (string.IsNullOrEmpty(model.Password))
            {
                return Json(new
                {
                    Succeeded = false,
                    Errors = new dynamic[] {
                        new {
                            Code = "NullReferenceException",
                            Description = "Password is null or empty"
                        }
                    }
                });
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Json(new
                {
                    Succeeded = true,
                    Token = GenerateJwtToken(model.Email, user)
                });
            }

            return Json(result);
        }

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
