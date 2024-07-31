using Absensi.Microservice.Models;

namespace Absensi.Microservice.Services.Absensi
{
    public interface IAbsensiService
    {
        Task<IEnumerable<AbsensiModel>> GetAll();
        Task<AbsensiModel>? GetById(int id);
        Task<IEnumerable<AbsensiModel>> GetByKaryawanId(int karyawanId);
        Task<IEnumerable<AbsensiModel>> GetByDate(DateOnly dateIn);
        Task<AbsensiModel> Create(int karyawanId);
        Task<AbsensiModel>? ExitTime(int karyawanId);
    }
}
