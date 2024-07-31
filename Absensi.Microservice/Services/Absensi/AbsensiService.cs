using Absensi.Microservice.Models;
using Absensi.Microservice.Services.Karyawan;
using CommonLibrary;
using RepositoryLibrary;
using System.Diagnostics;

namespace Absensi.Microservice.Services.Absensi
{
    public class AbsensiService : IAbsensiService
    {
        private readonly IRepository<AbsensiModel> _repository;
        private readonly IKaryawanService _karyawanService;
        private readonly TimeOnly JADWAL_MASUK0 = new TimeOnly(7, 0);
        private readonly TimeOnly JADWAL_KELUAR0 = new TimeOnly(13, 0);
        private readonly TimeOnly JADWAL_MASUK1 = new TimeOnly(13, 0);
        private readonly TimeOnly JADWAL_KELUAR1 = new TimeOnly(19, 0);
        private const int TOLERANSI_MASUK = 15;
        private const int TOLERANSI_KELUAR = 30;
        public AbsensiService(IRepository<AbsensiModel> repository, IKaryawanService karyawanService)
        {
            _repository = repository;
            _karyawanService = karyawanService;
        }
        public async Task<AbsensiModel> Create(int karyawanId)
        {
            try
            {
                var karyawan = await _karyawanService.GetById(karyawanId);
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                var absensiInfo = await _repository.FindBy(obj => obj.Karyawan.Id == karyawanId && dateNow.CompareTo(obj.DateIn) == 0);
                if (absensiInfo != null) throw new BadRequestException("Karyawan sudah absen masuk");

                var waktuMasuk = DateTime.Now;
                string statusMasuk;
                TimeSpan selisih;
                // shift pagi
                if (!karyawan.Shift)
                    selisih = waktuMasuk - DateTime.Today.Add(JADWAL_MASUK0.ToTimeSpan());
                //shift siang
                else
                    selisih = waktuMasuk - DateTime.Today.Add(JADWAL_MASUK1.ToTimeSpan());


                if (selisih.TotalMinutes < 0)
                {
                    statusMasuk = "Datang Lebih Awal";
                }
                else if (selisih.TotalMinutes >= 0 && selisih.TotalMinutes <= TOLERANSI_MASUK)
                {
                    statusMasuk = "Tepat Waktu";
                }
                else { statusMasuk = "Terlambat"; }

                var absensi = new AbsensiModel()
                {
                    Karyawan = karyawan,
                    DateIn = DateOnly.FromDateTime(DateTime.Now),
                    TimeIn = TimeOnly.FromDateTime(waktuMasuk),
                    StatusMasuk = statusMasuk,
                    StatusKeluar = ""
                };
                var result = await _repository.Save(absensi);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<AbsensiModel>? ExitTime(int karyawanId)
        {
            try
            {
                var karyawanInfo = await _karyawanService.GetById(karyawanId);
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                var absensiInfo = await _repository.FindBy(obj => obj.Karyawan.Id == karyawanId && dateNow.CompareTo(obj.DateIn) == 0);
                if (absensiInfo == null)
                    throw new BadHttpRequestException("Karyawan belum absen masuk"); ;
                if (absensiInfo.StatusKeluar != "")
                    throw new BadHttpRequestException("Karyawan sudah absen keluar"); ;


                var waktuKeluar = DateTime.Now;
                string statusKeluar;
                TimeSpan selisih;
                // shift pagi
                if (!karyawanInfo.Shift)
                    selisih = waktuKeluar - DateTime.Today.Add(JADWAL_KELUAR0.ToTimeSpan());
                else
                    selisih = waktuKeluar - DateTime.Today.Add(JADWAL_KELUAR1.ToTimeSpan());

                if (selisih.TotalMinutes < 0)
                {
                    statusKeluar = "Pulang Lebih Awal";
                }
                else if (selisih.TotalMinutes >= 0 && selisih.TotalMinutes <= TOLERANSI_KELUAR)
                {
                    statusKeluar = "Tepat Waktu";
                }
                else { statusKeluar = "Overtime"; }

                TimeSpan selisihMasukKeluar = TimeOnly.FromDateTime(waktuKeluar) - absensiInfo.TimeIn;



                var absensi = new AbsensiModel()
                {
                    Id = absensiInfo.Id,
                    Karyawan = absensiInfo.Karyawan,
                    DateIn = absensiInfo.DateIn,
                    TimeIn = absensiInfo.TimeIn,
                    StatusMasuk = absensiInfo.StatusMasuk,
                    TimeOut = TimeOnly.FromDateTime(waktuKeluar),
                    StatusKeluar = statusKeluar,
                    SelisihJamMasukKeluar = (int)selisihMasukKeluar.TotalHours

                };
                _repository.detach(absensiInfo);
                var result = await _repository.Update(absensi);
                return result;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new BadHttpRequestException(e.Message);
            }
        }

        public async Task<IEnumerable<AbsensiModel>> GetAll()
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

        public async Task<IEnumerable<AbsensiModel>> GetByDate(DateOnly dateIn)
        {
            try
            {
                if (dateIn > DateOnly.FromDateTime(DateTime.Now))
                    throw new BadHttpRequestException("Tanggal Input tidak boleh Lebih Besar dari Hari Ini!");
                return await _repository.FindByGroup(absensi => absensi.DateIn == dateIn);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<AbsensiModel>? GetById(int id)
        {
            try
            {

                var result = await _repository.FindById(id, ["Karyawan"]);
                if (result == null)
                    throw new NotFound("Absensi tidak ditemukan");
                return result;


            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AbsensiModel>> GetByKaryawanId(int karyawanId)
        {
            try
            {
                var result = await _repository.FindByGroup(absensi => absensi.Karyawan.Id == karyawanId, ["Karyawan"]);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}
