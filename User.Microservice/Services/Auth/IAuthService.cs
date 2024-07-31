using User.Microservice.DTO;

namespace User.Microservice.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginDTO user);

    }
}
