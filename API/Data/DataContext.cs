using API.Entintes;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}