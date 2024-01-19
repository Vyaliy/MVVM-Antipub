using Microsoft.EntityFrameworkCore;
using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.ViewModels
{
    public class ClosedNotesShowWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ClosedNote> ClosedNotes { get; set; }
        public ObservableCollection<Tariff> Tariffs { get; set; }
        public ClosedNotesShowWindowViewModel()
        {
            using (var db = new ApplicationContext())
            {
                //db.Database.EnsureCreated();
                db.ClosedNotes.Load();
                db.Tariffs.Load();
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
                Tariffs = db.Tariffs.Local.ToObservableCollection();

            }
        }
    }
}
