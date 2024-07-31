using Absensi.Microservice.DTO;
using Absensi.Microservice.Models;

namespace Absensi.Microservice.Services.Karyawan
{
    public interface IKaryawanService
    {
        Task<IEnumerable<KaryawanModel>> GetAll();
        Task<KaryawanModel>? GetById(int id);
        Task<KaryawanModel>? Update(UpdateKaryawanDTO karyawan);
        Task<CreateKaryawanDTO> Create(CreateKaryawanDTO karyawan);
        Task<int> Delete(int id);
    }
}
