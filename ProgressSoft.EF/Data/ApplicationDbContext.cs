using Microsoft.EntityFrameworkCore;
using ProgressSoft.Core.Entites;
using ProgressSoft.EF.Data.Configuration;
namespace ProgressSoft.EF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        #region Entites
        public DbSet<CardReader> CardRedaers { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CardReaderConfiguration).Assembly);
        }
    }
}
