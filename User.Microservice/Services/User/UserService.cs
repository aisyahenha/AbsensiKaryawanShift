using CommonLibrary;
using RepositoryLibrary;
using System.Diagnostics;
using User.Microservice.DTO;
using User.Microservice.Models;

namespace User.Microservice.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserModel> _repository;
        public UserService(IRepository<UserModel> repository)
        {
            _repository = repository;
        }

        public async Task<CreateUserDTO> Create(CreateUserDTO payload)
        {
            try
            {
                var user = new UserModel()
                {
                    Username = payload.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(payload.Password),
                    Role = payload.Role.ToUpper()
                };
                await _repository.Save(user);
                return payload;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var user = await GetById(id);

                await _repository.Delete(user);
                return id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            try
            {
                return await _repository.FindAll();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<UserModel>? GetById(int id)
        {
            try
            {
                var result = await _repository.FindById(id);
                if (result == null)
                    throw new NotFound("User tidak ditemukan");

                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<UserModel>? Update(UpdateUserDTO payload)
        {
            try
            {
                var findedUser = await _repository.FindBy((obj) => obj.Username == payload.Username);

                if (findedUser is null) throw new BadRequestException("username atau Old Password salah");

                // string oldpass = BCrypt.Net.BCrypt.HashPassword(payload.OldPassword);
                var isVerified = BCrypt.Net.BCrypt.Verify(payload.OldPassword, findedUser.Password);

                if (!isVerified) throw new BadRequestException("username atau Old Password salah");

                _repository.detach(findedUser);
                var userUpdate = new UserModel()
                {
                    Id = findedUser.Id,
                    Username = findedUser.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(payload.Password),
                    Role = findedUser.Role

                };

                return await _repository.Update(userUpdate);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}
