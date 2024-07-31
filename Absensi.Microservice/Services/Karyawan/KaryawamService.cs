using Absensi.Microservice.DTO;
using Absensi.Microservice.Models;
using CommonLibrary;
using RepositoryLibrary;
using System.Diagnostics;

namespace Absensi.Microservice.Services.Karyawan
{
    public class KaryawamService : IKaryawanService
    {
        private readonly IRepository<KaryawanModel> _repository;
        public KaryawamService(IRepository<KaryawanModel> repository)
        {
            _repository = repository;
        }
        public async Task<CreateKaryawanDTO> Create(CreateKaryawanDTO payload)
        {
            try
            {
                string ShiftDesc;

                if (payload.Shift)
                    ShiftDesc = "Shift Siang";
                else
                    ShiftDesc = "Shift Pagi";

                ;
                //DateOfBirthDTO date = new DateOfBirthDTO() 
                //{ 
                //    Year = payload.DateOfBirth.Year,
                //    Month = payload.DateOfBirth.Month,
                //    Day = payload.DateOfBirth.Day,
                //    DayOfWeek = payload.DateOfBirth.DayOfWeek
                //};

                DateOnly date = new DateOnly(payload.DateOfBirth.Year, payload.DateOfBirth.Month, payload.DateOfBirth.Day);

                var karyawan = new KaryawanModel()
                {
                    NIK = payload.NIK,
                    Name = payload.Name,
                    Address = payload.Address,
                    DateOfBirth = date,
                    Position = payload.Position,
                    Shift = payload.Shift,
                    ShiftDescription = ShiftDesc,
                    CratedAt = DateTime.UtcNow
                };
                await _repository.Save(karyawan);
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

        public async Task<IEnumerable<KaryawanModel>> GetAll()
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

        public async Task<KaryawanModel>? GetById(int id)
        {
            try
            {
                var result = await _repository.FindById(id);
                if (result == null)
                    throw new NotFound("Karyawan tidak ditemukan");

                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<KaryawanModel>? Update(UpdateKaryawanDTO payload)
        {

            try
            {
                string ShiftDesc;

                if (payload.Shift)
                    ShiftDesc = "Shift Siang";
                else
                    ShiftDesc = "Shift Pagi";

                DateOnly date = new DateOnly(payload.DateOfBirth.Year, payload.DateOfBirth.Month, payload.DateOfBirth.Day);

                var karyawanInfo = await GetById(payload.Id);
                _repository.detach(karyawanInfo);
                var karyawan = new KaryawanModel()
                {
                    Id = payload.Id,
                    NIK = payload.NIK,
                    Name = payload.Name,
                    Address = payload.Address,
                    DateOfBirth = date,
                    Position = payload.Position,
                    Shift = payload.Shift,
                    ShiftDescription = ShiftDesc,
                    UpdatedAt = DateTime.UtcNow,
                    CratedAt = karyawanInfo.CratedAt
                };

                return await _repository.Update(karyawan);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }



    }
}
