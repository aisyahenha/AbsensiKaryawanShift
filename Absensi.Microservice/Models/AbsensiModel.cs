using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absensi.Microservice.Models
{
    [Table("m_absensi")]
    public class AbsensiModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Id Karyawan Cannot ne empty!")]
        public KaryawanModel Karyawan { get; set; }
        public DateOnly DateIn { get; set; }
        public TimeOnly TimeIn { get; set; }
        public string StatusMasuk { get; set; }
        public TimeOnly TimeOut { get; set; }
        public string StatusKeluar { get; set; }
        public int SelisihJamMasukKeluar { get; set; }


    }
}
