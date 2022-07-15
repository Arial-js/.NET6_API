using EsercitazioneAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EsercitazioneAPI.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UsersModel>()
                .Property(x => x.Role)
                .HasConversion(
                v => v.ToString(),
                v => (UsersRolesModel)Enum.Parse(typeof(UsersRolesModel), v));
        }

        public DbSet<UsersModel> Users { get; set; }
    }
}
