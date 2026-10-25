using Microsoft.EntityFrameworkCore;
using WebApplication_Store.Models;

namespace WebApplication_Store.Models
{
    public class ApplicationModel : DbContext
    {
        public DbSet<Goods> Good { get; set; } = null;

        public ApplicationModel(DbContextOptions<ApplicationModel> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных 
        }
    }
}
