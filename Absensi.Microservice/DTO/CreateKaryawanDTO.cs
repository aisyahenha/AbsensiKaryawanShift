using System.ComponentModel.DataAnnotations;

namespace Absensi.Microservice.DTO
{
    public class CreateKaryawanDTO
    {
        [Required]
        [MaxLength(10)]
        public string NIK { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        //public DateOnly DateOfBirth { get; set; }
        public DateOfBirthDTO DateOfBirth { get; set; }
        [Required]
        public string Position { get; set; }
        public bool Shift { get; set; } = false;
    }
}
