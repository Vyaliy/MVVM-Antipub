using Microsoft.IdentityModel.Abstractions;
using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using MVVM_Antipub.Models.Repositories;
using MVVM_Antipub.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_Antipub.ViewModels
{
    public class NewNoteViewModel : ViewModelBase
    {
        public CurrentNote Cn { get; set; }
        public ObservableCollection<Tariff> Tariffs { get; set; }
        public Tariff ChosenTariff { get; set; }
        public new MainWindowViewModel parentViewModel { get; set; }
        public RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                {
                    if (parentViewModel.CurrentNotes.FirstOrDefault(x => x.CardNumber == Cn.CardNumber) != (CurrentNote)default)
                    {
                        return;
                    }
                    else
                    {
                        Cn.ArrivalTime = DateTime.Now;
                        Cn.Tariff = ChosenTariff;
                        Cn.TariffId = ChosenTariff.Id;
                        parentViewModel.CurrentNotes.Add(Cn);
                        CloseThis();
                    }
                });
            }
        }
        public NewNoteViewModel(MainWindowViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;
            using (var db = new ApplicationContext())
            {
                Tariffs = db.Tariffs.ReadAll();
            }
            ChosenTariff = new Tariff();
            Cn = new CurrentNote();
            Cn.ShiftId = this.parentViewModel.Sh.Id;
        }
    }
}
