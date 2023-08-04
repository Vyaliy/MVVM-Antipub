using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MVVM_Antipub.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.Models
{
    public class ApplicationContext : DbContext
    {
        private DbSet<Tariff> tariffs;

        public DbSet<ClosedNote> ClosedNotes { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<RegularCustomer> RegularCustomers { get; set; }
        public DbSet<Tariff> Tariffs
        {
            get
            {
                foreach (var t in tariffs)
                    t.Hours = Hours.Where(x => x.TariffId == t.Id).ToList();
                return tariffs;
            }

            set
            {
                tariffs = value;
            }
        }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=Antipub.db");
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TimeCafe;Trusted_Connection=True;TrustServerCertificate=True;",
                options => options.EnableRetryOnFailure());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClosedNote>().Property(s => s.PastTime)
                .HasConversion(new TimeSpanToTicksConverter()); // or TimeSpanToStringConverter
        }
    }
}
