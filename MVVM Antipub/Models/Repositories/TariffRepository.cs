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
        public static ObservableCollection<Tariff> ReadAll(this DbSet<Tariff> Tariffs, bool withOldTariffs = false)
        {
            if (withOldTariffs)
            {
                Tariffs.Load();
                return Tariffs.Local.ToObservableCollection();
            }
            Tariffs.Load();
            ObservableCollection<Tariff> TariffsObs = new ObservableCollection<Tariff>();
            foreach (var Tariff in Tariffs)
            {
                if (Tariff.InUse) 
                    TariffsObs.Add(Tariff);
            }
            return TariffsObs;
        }
        public static void Insert (this DbSet<Tariff> Tariffs, Tariff tariff)
        {
            Tariffs.Add(tariff);
        }
        public static void Delete (this DbSet<Tariff> Tariffs, Tariff tariff, bool ensureDelete = false)
        {
            if (ensureDelete)
            {
                Tariffs.Remove(tariff);
                return;
            }
            Tariffs.Load();
            Tariffs.Local.First(tariff1 => tariff1.Id == tariff.Id).InUse = false;
        }
    }
}