using Microsoft.EntityFrameworkCore;
using User.Microservice.Models;

namespace User.Microservice.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> User { get; set; }
    }
}
