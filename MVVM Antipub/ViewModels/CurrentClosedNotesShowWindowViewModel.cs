using Microsoft.EntityFrameworkCore;
using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVM_Antipub.ViewModels
{
    public class CurrentClosedNotesShowWindowViewModel : ViewModelBase
    {
        public void Initialize()
        {
            using (var db = new ApplicationContext())
            {
                //db.Database.EnsureCreated();
                db.ClosedNotes.Load();
                db.Tariffs.Load();
                db.Shifts.Load();
                db.RegularCustomers.Load();
                int LastShift = db.Shifts.Max(x => x.Id);
                /*
                foreach (var note in db.ClosedNotes)
                {
                    note.Tariff = db.Tariffs.First(x => x.Id == note.TariffId);
                }
                */
                foreach (var note in db.ClosedNotes)
                {
                    //Сделать имя гостя!!!!!!!!!!!!

                }
                ClosedNotes = db.ClosedNotes.Local.ToObservableCollection();
                var a = ClosedNotes.Where(x => x.ShiftId == LastShift).ToList();
                ClosedNotes = new ObservableCollection<ClosedNote>(a);
                Tariffs = db.Tariffs.Local.ToObservableCollection();
                RegularCustomers = db.RegularCustomers.Local.ToObservableCollection();

            }
        }
        public ObservableCollection<ClosedNote> ClosedNotes { get; set; }
        public ObservableCollection<Tariff> Tariffs { get; set; }
        public ObservableCollection<RegularCustomer> RegularCustomers { get; set; }
    }
}