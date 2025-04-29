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
                db.Tariffs.Load();
                db.Shifts.Load();
                db.RegularCustomers.Load();

                int lastShiftId = db.Shifts.Max(x => x.Id);

                var notes = db.ClosedNotes
                    .Include(x => x.Tariff)
                    .Include(x => x.RegularCustomer)
                    .Where(x => x.ShiftId == lastShiftId)
                    .ToList();

                ClosedNotes = new ObservableCollection<ClosedNote>(notes);
                Tariffs = db.Tariffs.Local.ToObservableCollection();
                RegularCustomers = db.RegularCustomers.Local.ToObservableCollection();
            }
        }
        public ObservableCollection<ClosedNote> ClosedNotes { get; set; }
        public ObservableCollection<Tariff> Tariffs { get; set; }
        public ObservableCollection<RegularCustomer> RegularCustomers { get; set; }
    }
}