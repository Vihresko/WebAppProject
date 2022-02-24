using Microsoft.EntityFrameworkCore;
using WorkDiaryDB.Models;

namespace WorkDiaryDB
{
    public class WorkDiaryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-EQU1II1\SQLEXPRESS;Database=TestWorkDiary;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientProcedure>().HasKey(cp => new { cp.ClientId, cp.ProcedureId, cp.UserId });
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClientProcedure> ClientProcedures { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}