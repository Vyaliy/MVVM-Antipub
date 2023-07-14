using MVVM_Antipub.Models.Database;
using Microsoft.EntityFrameworkCore;
using MVVM_Antipub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Antipub.Models.Repositories;
using System.Windows.Input;
using System.Xml.Linq;
using MVVM_Antipub.Views;
using MVVM_Antipub.Views.Windows;

namespace MVVM_Antipub.ViewModels
{
    public class TariffChangeViewModel : ViewModelBase
    {
        private ObservableCollection<Tariff> tariffs;
        public ObservableCollection<Tariff> Tariffs 
        {
            get
            {
                return tariffs;
            }
            set
            {
                tariffs = value;
                OnPropertyChanged("Tariffs");
            }
        }
        public new MainWindowViewModel parentViewModel { get; set; }
        public TariffChangeViewModel(MainWindowViewModel parentViewModel) 
        {
            base.parentViewModel = parentViewModel;
            Tariffs = new ObservableCollection<Tariff>();
            using (var db = new ApplicationContext())
            {
                //db.Database.EnsureDeleted();
                Tariffs = db.Tariffs.ReadAll();
            }
        }

        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                {
                    NewTariffWindow wndNewTariff = new NewTariffWindow(this);
                    wndNewTariff.ShowDialog();
                    using (var db = new ApplicationContext())
                    {
                        db.Database.EnsureDeleted();
                        db.Tariffs.Add(Tariffs.Last());
                        db.SaveChanges();
                    }
                });
            }
        }
    }
}
