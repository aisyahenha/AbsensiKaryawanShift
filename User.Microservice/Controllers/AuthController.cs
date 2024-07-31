using CommonLibrary;
using Microsoft.AspNetCore.Mvc;
using User.Microservice.DTO;
using User.Microservice.Services.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.Microservice.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<SuccessResponse<LoginResponseDTO>> Login(LoginDTO payload)
        {
            LoginResponseDTO result = await _authService.Login(payload);
            return new SuccessResponse<LoginResponseDTO>()
            {
                Status = true,
                Message = "Login Sukses",
                Data = result
            };
        }


    }
}
