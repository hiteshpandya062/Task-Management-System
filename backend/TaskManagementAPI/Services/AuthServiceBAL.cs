using DataAccessLayer;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagementAPI.Helper;
using TaskManagementAPI.Interface;
using TaskManagementAPI.SharedResponses;

namespace TaskManagementAPI.Services
{
    public class AuthServiceBAL : IAuthServiceBAL
    {
        private readonly IAuthServiceDAL _authServiceDAL;
        private readonly IUserServiceDAL _userServiceDAL;
        public readonly AppSettings _appSettings;
        public AuthServiceBAL(IOptions<AppSettings> appSettings, IAuthServiceDAL authServiceDAL, IUserServiceDAL userServiceDAL)
        {
            _authServiceDAL = authServiceDAL;
            _userServiceDAL = userServiceDAL;
            _appSettings = appSettings.Value;
        }

        public async Task<ApiResponse<bool>> Register(UsersRegisterVM usersRegister)
        {
            if (usersRegister == null)
            {
                return new ApiResponse<bool>
                {
                    Result = false,
                    Errors = new List<string>() { "User details missing." },
                    Message = "User details missing.",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            User user = new User()
            {
                createdAt = DateTime.Now,
                email = usersRegister.Email,
                role = usersRegister.Role,
                username = usersRegister.Userame,
                password = PasswordHelper.HashPassword(usersRegister.Password),
            };
            var result = await _authServiceDAL.Register(user);

            return new ApiResponse<bool>
            {
                Result = true,
                Errors = new List<string>(),
                Message = "Registered successfully.",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ApiResponse<string>> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new ApiResponse<string>
                {
                    Result = string.Empty,
                    Message = "Email or password is null or empty.",
                    Errors = new List<string>() { "Email or password is null or empty." },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var user = await _userServiceDAL.GetUserByEmail(email);
            if (user == null)
                return new ApiResponse<string>
                {
                    Result = string.Empty,
                    Message = "User not found with the provided email.",
                    Errors = new List<string>() { "User not found with the provided email." },
                    StatusCode = StatusCodes.Status404NotFound
                };

            var verifyPassword = PasswordHelper.VerifyHashedPassword(user.password, password);
            if (!verifyPassword)
                return new ApiResponse<string>
                {
                    Result = string.Empty,
                    Message = "Password mismatch.",
                    Errors = new List<string>() { "Password mismatch." },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            string token = GenerateJwtToken(user.id, user.email, user.role, _appSettings.JwtKey,  _appSettings.Issuer,  _appSettings.Audience,  _appSettings.ExpireMinutes);

            return new ApiResponse<string>
            {
                Result = token,
                Message = "User authenticated. Token Generated.",
                Errors = new List<string>(),
                StatusCode = StatusCodes.Status200OK
            };

        }

        private static string GenerateJwtToken(int userId, string email, Roles role, string secretKey, string issuer, string audience, int expireMinutes = 60)
        {
            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException(nameof(secretKey), "JWT Secret Key cannot be null or empty.");

            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            if (keyBytes.Length < 16)
                throw new ArgumentException("JWT Secret Key must be at least 16 characters (128 bits).", nameof(secretKey));

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.NameId, userId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, email),
        new Claim(ClaimTypes.Role, role.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
