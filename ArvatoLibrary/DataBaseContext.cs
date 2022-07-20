using ArvatoLibrary.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ArvatoLibrary {
    public class DataBaseContext : DbContext {

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyDate> CurrencyDates { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=ArvatoDB;TrustServerCertificate=True;Authentication='Active Directory Integrated'");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Currency>().ToTable("Currency");
            modelBuilder.Entity<CurrencyDate>().ToTable("CurrencyDate");
            modelBuilder.Entity<CurrencyRate>().ToTable("CurrencyRate");

            modelBuilder.Entity<Currency>(builder =>
            {
                builder.HasKey(entity => entity.Sequence);
                builder.Property(e => e.Sequence).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CurrencyDate>(builder => {
                builder.HasKey(entity => entity.Sequence);
                builder.Property(e => e.Sequence).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CurrencyRate>(builder => {
                builder.HasKey(entity => entity.Sequence);
                builder.Property(e => e.Sequence).ValueGeneratedOnAdd();
            });
        }
    }
}
