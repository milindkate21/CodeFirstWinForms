using Microsoft.EntityFrameworkCore;

namespace CodeFirstWinForms.Datamodel
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-CGR3DT4;Database=Test;Trusted_Connection=True;");
        }
        public DbSet<StudentInfo> StudentInfo { get; set; }
        public DbSet<Country> Country { get; set; }
    }
}
