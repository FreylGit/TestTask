using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=TestTask;Trusted_Connection=True;");

        }

    }
}
