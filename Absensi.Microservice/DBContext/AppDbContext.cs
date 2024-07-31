using Absensi.Microservice.Models;
using Microsoft.EntityFrameworkCore;


namespace Absensi.Microservice.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<KaryawanModel> Karyawan { get; set; }
        public DbSet<AbsensiModel> Absensi { get; set; }
    }
}
