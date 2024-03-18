using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VendingMachineAPIs.BLL.DTOs;
using VendingMachineAPIs.BLL.DTOs.UserDTOs;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.BLL.Services.AuthService
{
    public class TokenService : IToken
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public TokenService(UserManager<User> userManager,
                                    IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<GetUserResponseDTO?> GenerateLoginResponseWithToken(LoginDTOModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                

                return new GetUserResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireDate = token.ValidTo,
                    UserName = user.UserName,
                    UserId = user.Id,
                };
            }
            return null;
        }
        public string GetUserIdFromToken(string token)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authSigningKey,
                ValidateLifetime = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var validatedToken = tokenHandler.ValidateToken(token, validationParameters, out _);

            var userIdClaim = validatedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
            else
                return String.Empty;
           
        }
        public string ExtractToken(HttpRequest _request)
        {
            string _tokenWithType = _request.Headers["Authorization"];
            return _tokenWithType.Replace("Bearer ", "");
        }
    }
}
