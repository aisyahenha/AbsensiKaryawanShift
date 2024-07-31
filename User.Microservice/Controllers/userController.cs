using CommonLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Microservice.DTO;
using User.Microservice.Models;
using User.Microservice.Services.User;

namespace User.Microservice.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class userController : ControllerBase
    {
        private readonly IUserService _userService;
        public userController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Policy = "admin")]
        [HttpGet("get-all")]
        public async Task<SuccessResponse<IEnumerable<UserModel>>> GetAll()
        {
            IEnumerable<UserModel> result = await _userService.GetAll();
            return new SuccessResponse<IEnumerable<UserModel>>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }
        [Authorize(Policy = "admin")]
        [HttpGet("detail/{id}")]
        public async Task<SuccessResponse<UserModel>> Details(int id)
        {
            UserModel result = await _userService.GetById(id);
            return new SuccessResponse<UserModel>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        [Authorize(Policy = "admin")]
        [HttpPost("create")]
        public async Task<SuccessResponse<CreateUserDTO>> Create(CreateUserDTO payload)
        {
            CreateUserDTO result = await _userService.Create(payload);
            return new SuccessResponse<CreateUserDTO>()
            {
                Status = true,
                Message = "Create Sukses",
                Data = result
            };
        }
        [HttpPatch("update")]
        [Authorize(Policy = "all")]
        public async Task<SuccessResponse<UserModel>> Update(UpdateUserDTO payload)
        {
            UserModel result = await _userService.Update(payload);
            return new SuccessResponse<UserModel>()
            {
                Status = true,
                Message = "Update Sukses",
                Data = result
            };
        }
        [Authorize(Policy = "admin")]
        [HttpDelete("delete")]
        public async Task<SuccessResponse<int>> Delete(int id)
        {
            int result = await _userService.Delete(id);
            return new SuccessResponse<int>()
            {
                Status = true,
                Message = "Delete Sukses",
                Data = result
            };
        }


    }
}
