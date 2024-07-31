using User.Microservice.DTO;
using User.Microservice.Models;

namespace User.Microservice.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel>? GetById(int id);
        Task<UserModel>? Update(UpdateUserDTO user);
        Task<CreateUserDTO> Create(CreateUserDTO user);
        Task<int> Delete(int id);

    }
}
