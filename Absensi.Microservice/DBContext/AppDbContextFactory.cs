using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Absensi.Microservice.DBContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //optionsBuilder.UseSqlServer()
            optionsBuilder.UseSqlServer(connectionString);

            //optionsBuilder.UseSqlServer("data source=iqrom-pavilion\\SQLEXPRESS;initial catalog=AbsensiKaryawan;trusted_connection=true");
            return new AppDbContext(optionsBuilder.Options);


        }
    }
}
