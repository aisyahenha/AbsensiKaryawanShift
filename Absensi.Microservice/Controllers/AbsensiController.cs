using Absensi.Microservice.Models;
using Absensi.Microservice.Services.Absensi;
using CommonLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Absensi.Microservice.Controllers
{
    [Route("api/absensi")]
    [ApiController]
    public class AbsensiController : ControllerBase
    {
        private readonly IAbsensiService _absensiService;
        public AbsensiController(IAbsensiService absensiService)
        {
            _absensiService = absensiService;
        }

        // GET: All 
        [Authorize(Policy = "admin")]
        [HttpGet("get-all")]
        public async Task<SuccessResponse<IEnumerable<AbsensiModel>>> GetAll()
        {
            IEnumerable<AbsensiModel> result = await _absensiService.GetAll();

            return new SuccessResponse<IEnumerable<AbsensiModel>>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }
        [Authorize(Policy = "all")]
        // GET: KaryawanController/Details/5
        [HttpGet("detail/{id}")]
        public async Task<SuccessResponse<AbsensiModel>> Details(int id)
        {
            AbsensiModel result = await _absensiService.GetById(id);
            return new SuccessResponse<AbsensiModel>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        [Authorize(Policy = "all")]
        //GET BY KARYAWAN ID
        [HttpGet("detail-karyawan/{id}")]
        public async Task<SuccessResponse<IEnumerable<AbsensiModel>>> KaryawanDetails(int id)
        {
            IEnumerable<AbsensiModel> result = await _absensiService.GetByKaryawanId(id);
            return new SuccessResponse<IEnumerable<AbsensiModel>>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }


        // POST: Create
        [HttpPost("absen-in/{idKaryawan}")]
        public async Task<SuccessResponse<AbsensiModel>> Create(int idKaryawan)
        {
            AbsensiModel result = await _absensiService.Create(idKaryawan);
            return new SuccessResponse<AbsensiModel>()
            {
                Status = true,
                Message = "Create Sukses",
                Data = result
            };
        }
        // post: time Out
        [HttpPost("absen-out/{idKaryawan}")]
        public async Task<SuccessResponse<AbsensiModel>> ExitTime(int idKaryawan)
        {
            AbsensiModel result = await _absensiService.ExitTime(idKaryawan);
            return new SuccessResponse<AbsensiModel>()
            {
                Status = true,
                Message = "Update Sukses",
                Data = result
            };
        }
    }
}
