using Microsoft.EntityFrameworkCore;
using MVVM_Antipub.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.Models.Repositories
{
    public static class TariffRepository
    {
        public static ObservableCollection<Tariff> ReadAll(this DbSet<Tariff> db)
        {
            //return db.Local.ToObservableCollection();
            return new ObservableCollection<Tariff>(
                db.Include(t => t.Hours)
                .AsNoTracking()
                .ToList()
            );
        }
        public static void Insert (this DbSet<Tariff> db, Tariff tariff)
        {
            db.Add(tariff);
        }
        public static void Delete (this DbSet<Tariff> db, Tariff tariff)
        {
            db.Remove(tariff);
        }
    }
}