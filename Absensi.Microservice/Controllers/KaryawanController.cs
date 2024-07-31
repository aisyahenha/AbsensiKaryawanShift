using Absensi.Microservice.DTO;
using Absensi.Microservice.Models;
using Absensi.Microservice.Services.Karyawan;
using CommonLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Absensi.Microservice.Controllers
{

    [ApiController]
    [Route("api/karyawan")]
    public class KaryawanController : ControllerBase
    {
        private readonly IKaryawanService _karyawanService;
        public KaryawanController(IKaryawanService karyawanService)
        {
            _karyawanService = karyawanService;
        }

        // GET: KaryawanAll
        [Authorize(Policy = "admin")]
        [HttpGet("get-all")]
        public async Task<SuccessResponse<IEnumerable<KaryawanModel>>> GetAll()
        {
            IEnumerable<KaryawanModel> result = await _karyawanService.GetAll();
            return new SuccessResponse<IEnumerable<KaryawanModel>>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        // GET: KaryawanController/Details/5
        [Authorize(Policy = "all")]
        [HttpGet("detail/{id}")]
        public async Task<SuccessResponse<KaryawanModel>> Details(int id)
        {
            KaryawanModel result = await _karyawanService.GetById(id);
            return new SuccessResponse<KaryawanModel>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        // POST: KaryawanController/Create
        [Authorize(Policy = "admin")]
        [HttpPost("create")]
        public async Task<SuccessResponse<CreateKaryawanDTO>> Create(CreateKaryawanDTO payload)
        {
            DateOnly date = new DateOnly(payload.DateOfBirth.Year, payload.DateOfBirth.Month, payload.DateOfBirth.Day);
            DayOfWeek actualDayOfWeek = date.DayOfWeek;
            payload.DateOfBirth.DayOfWeek = actualDayOfWeek;
            CreateKaryawanDTO result = await _karyawanService.Create(payload);
            return new SuccessResponse<CreateKaryawanDTO>()
            {
                Status = true,
                Message = "Create Sukses",
                Data = result
            };
        }

        // PATCH: Update
        [Authorize(Policy = "all")]
        [HttpPatch("update")]
        public async Task<SuccessResponse<KaryawanModel>> Update(UpdateKaryawanDTO payload)
        {
            DateOnly date = new DateOnly(payload.DateOfBirth.Year, payload.DateOfBirth.Month, payload.DateOfBirth.Day);
            DayOfWeek actualDayOfWeek = date.DayOfWeek;
            payload.DateOfBirth.DayOfWeek = actualDayOfWeek;
            KaryawanModel result = await _karyawanService.Update(payload);
            return new SuccessResponse<KaryawanModel>()
            {
                Status = true,
                Message = "Update Sukses",
                Data = result
            };
        }

        // DELETE
        [Authorize(Policy = "admin")]
        [HttpDelete("delete")]
        public async Task<SuccessResponse<int>> Delete(int id)
        {
            int result = await _karyawanService.Delete(id);
            return new SuccessResponse<int>()
            {
                Status = true,
                Message = "Delete Sukses",
                Data = result
            };
        }

    }
}