using CommonLibrary;
using Microsoft.IdentityModel.Tokens;
using RepositoryLibrary;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Microservice.DTO;
using User.Microservice.Models;

namespace User.Microservice.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserModel> _repository;
        public AuthService(IRepository<UserModel> repository)
        {
            _repository = repository;
        }
        public async Task<LoginResponseDTO> Login(LoginDTO user)
        {
            var findedUser = await _repository.FindBy((obj) => obj.Username == user.Username);

            if (findedUser is null) throw new BadRequestException("username atau password salah");

            var isVerified = BCrypt.Net.BCrypt.Verify(user.Password, findedUser.Password);

            if (!isVerified) throw new BadRequestException("username atau password salah");

            var claims = new List<Claim>
            {
                new Claim("Username", $"{findedUser.Username}-{findedUser.Id}" ),
                new Claim(ClaimTypes.Role,findedUser.Role),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, findedUser.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ini secret key JWT yang panjang sekali hardcode");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username,
                Role = findedUser.Role
            };

            return result;
        }



    }
}
