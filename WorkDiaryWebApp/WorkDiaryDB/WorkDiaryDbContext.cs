using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.WorkDiaryDB
{
    public class WorkDiaryDbContext : IdentityDbContext<User>
    {
        public WorkDiaryDbContext(DbContextOptions options)
            : base(options) { }
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
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ClientProcedure> ClientProcedures { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Outcome> Outcomes { get; set; }
        public DbSet<Bank> Banks { get; set; }

        public DbSet<VisitBag> VisitBags { get; set; }

    }
}