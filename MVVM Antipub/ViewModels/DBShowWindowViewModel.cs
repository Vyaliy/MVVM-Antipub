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
    public class DBShowWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ClosedNote> ClosedNotes { get; set; }

        public DBShowWindowViewModel()
        {
            using (var db = new ApplicationContext())
            {

                db.Database.EnsureCreated();
                db.ClosedNotes.Load();
                ClosedNotes = db.ClosedNotes.Local.ToObservableCollection();
            }
        }
    }
}
