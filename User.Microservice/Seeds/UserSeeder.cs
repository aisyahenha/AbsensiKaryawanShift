using User.Microservice.DBContext;
using User.Microservice.Models;

namespace User.Microservice.Seeds
{
    public class UserSeeder
    {
        public static void Initialize(AppDbContext context)
        {

            var totalUser = context.User.ToList();

            if (totalUser.Count == 0)
            {
                var user = new UserModel()
                {
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "ADMIN"
                };

                context.User.Add(user);
                context.SaveChanges();
            }
        }
    }
}
