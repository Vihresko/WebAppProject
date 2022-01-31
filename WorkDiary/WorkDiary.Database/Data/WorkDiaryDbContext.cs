using Microsoft.EntityFrameworkCore;
using WorkDiary.Database.Data.Models;

namespace WorkDiary.Database.Data
{
    public class WorkDiaryDbContext :DbContext
    {
        public WorkDiaryDbContext()
        {

        }
        public WorkDiaryDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Client>()
           .HasIndex(u => u.Email)
           .IsUnique();
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
    }
}
