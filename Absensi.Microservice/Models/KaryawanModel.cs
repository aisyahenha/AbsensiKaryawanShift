using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absensi.Microservice.Models
{
    [Table("m_karyawan")]
    [Index(nameof(NIK), IsUnique = true)]
    public class KaryawanModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "NIK Cannot ne empty!")]
        public string NIK { get; set; }
        [Required(ErrorMessage = "Name Cannot ne empty!")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "DOB Cannot ne empty!")]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = "Position Cannot ne empty!")]
        public string Position { get; set; }
        public bool Shift { get; set; }
        public string ShiftDescription { get; set; }
        public DateTime CratedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
