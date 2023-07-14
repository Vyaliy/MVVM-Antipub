using Microsoft.EntityFrameworkCore;
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
        public DbSet<ClosedNote> ClosedNotes { get; set; }
        public DbSet<Hour> Minutes { get; set; }
        public DbSet<RegularCustomer> RegularCustomers { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=Antipub.db");
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-B5LD0OU;Server=.\SQLEXPRESS;Database=TimeCafe;Trusted_Connection=True");

        }
    }
}
