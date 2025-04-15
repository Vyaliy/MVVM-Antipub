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
        private MainWindowViewModel _mainViewModel;

        public void Initialize(MainWindowViewModel mainWindowViewModel)
        {
            _mainViewModel = mainWindowViewModel;
        }

        private Tariff selectedTariff;
        public Tariff SelectedTariff
        {
            get => selectedTariff;
            set
            {
                selectedTariff = value;
                OnPropertyChanged(nameof(SelectedTariff));
            }
        }
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
                return addCommand ??= new RelayCommand(obj =>
                {
                    NewTariffWindow wndNewTariff = new NewTariffWindow(this);
                    wndNewTariff.ShowDialog();

                    using (var db = new ApplicationContext())
                    {
                        db.Tariffs.Add(Tariffs.Last());
                        db.SaveChanges();
                    }
                });
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(_ =>
                {
                    if (SelectedTariff != null)
                    {
                        using (var db = new ApplicationContext())
                        {
                            var entity = db.Tariffs.Include(t => t.Hours).FirstOrDefault(t => t.Id == SelectedTariff.Id);
                            if (entity != null)
                            {
                                db.Tariffs.Remove(entity);
                                db.SaveChanges();
                            }
                        }
                        Tariffs.Remove(SelectedTariff);
                        SelectedTariff = null;
                    }
                },
                _ => SelectedTariff != null);
            }
        }
    }
}
