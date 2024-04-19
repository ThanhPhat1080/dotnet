using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using trainingEF.Entities;
using trainingEF.Models;
using trainingEF.Models.DTOs;

namespace trainingEF.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string secretTokenKey;

        public IdentityRepository(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration) {
            _userManager = userManager;
            secretTokenKey = configuration.GetSection("JwtConfig:Secret").Value;
        }

        public async Task<AuthResult> Register(UserRegistrationRequestDto userDto)
        {
            // Check if the email already exist
            var userExist = await _userManager.FindByEmailAsync(userDto.Email);

            if (userExist != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email already exist!"
                    }
                };
            }

            var newUser = new IdentityUser()
            {
                Email = userDto.Email,
                UserName = userDto.Name
            };

            var is_created = await _userManager.CreateAsync(newUser, userDto.Password);

            if (is_created.Succeeded)
            {

                // Assign role "User" as default
                var assignRoleResult = await _userManager.AddToRoleAsync(newUser, Roles.User.ToString());

                if (!assignRoleResult.Succeeded)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Cannot assign user role!"
                        }
                    };
                }

                return new AuthResult()
                {
                    Result = true,
                    Token = GenerateJwtToken(newUser),
                };
            }

            return new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Cannot create user!"
                }
            };
        }

        public async Task<AuthResult> Login(UserLoginRequestDto userDto)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(userDto.Email);

                if (userExist == null)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email does not exist!"
                        }
                    };
                }

                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(userExist, userDto.Password);
                if (isPasswordCorrect)
                {
                    // Generate the token
                    var token = GenerateJwtToken(userExist);

                    return new AuthResult()
                    {
                        Result = true,
                        Token = token,
                    };
                }

                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email or password is not correct!"
                    }
                };
            }
            catch (Exception)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Server error!"
                    }
                };
            }
        }

        public async Task<AuthResult> AddRoleAdmin(string userId)
        {
            try
            {
                // Check if the email already exist
                var userExist = await _userManager.FindByIdAsync(userId);

                if (userExist == null)
                {
                    return new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "User does not exist!"
                        }
                    };
                }
                var userRoles = _userManager.GetRolesAsync(userExist).Result;

                if (!userRoles.Contains(Roles.Admin.ToString()))
                {
                    //await _userManager.RemoveFromRoleAsync(user_exist, Roles.User.ToString());
                    await _userManager.AddToRoleAsync(userExist, Roles.Admin.ToString());
                    await _userManager.UpdateAsync(userExist);

                    return new AuthResult()
                    {
                        Result = true
                    };
                }

                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Cannot update user role!"
                    }
                };

            }
            catch (Exception)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Cannot update user role!"
                    },
                };
            }
        }
        private async Task<List<Claim>> CreateClaimAsync(IdentityUser user)
        {
            List<Claim> claims = new()
        {
            new(type: "Id", value: user.Id),
            new(type: JwtRegisteredClaimNames.Sub, value: user.Email),
            new(type: JwtRegisteredClaimNames.Email, value: user.Email),
            new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(type: JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString())
        };

            var userRoles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userRoles.Select(x => new Claim(type: ClaimTypes.Role, value: x)));

            return claims;
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(secretTokenKey);

            var claims = CreateClaimAsync(user).Result;

            // Token descriptor. (JWT payload)
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
