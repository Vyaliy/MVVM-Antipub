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
    public class TariffWindowViewModel : ViewModelBase
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
        private List<Hour> hours;
        public List<Hour> Hours
        {
            get { return hours; }
            set { hours = value; }
        }
        private Tariff selectedTariff;
        public Tariff SelectedTariff
        {
            get { return selectedTariff; }
            set
            {
                selectedTariff = value;
                OnPropertyChanged("SelectedTariff");
            }
        }
        public new MainWindowViewModel parentViewModel { get; set; }
        public TariffWindowViewModel(MainWindowViewModel parentViewModel) 
        {
            base.parentViewModel = parentViewModel;
            Tariffs = new ObservableCollection<Tariff>();
            using (var db = new ApplicationContext())
            {
                foreach (var t in db.Tariffs.ReadAll())
                {
                    Tariffs.Add(t);
                }
                Hours = db.Hours.ToList();
                //a.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Tariffs));
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
                    if (Tariffs.Count > 0)
                    {
                        /*
                        using (var db = new ApplicationContext())
                        {
                            if (!db.Tariffs.Contains(Tariffs.Last()))
                            {
                                db.Tariffs.Insert(Tariffs.Last());
                                db.SaveChanges();
                            }
                        }
                        */
                    }
                });
            }
        }
        public RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Tariff tariff = obj as Tariff;
                        if (tariff != null)
                        {
                            using (var db = new ApplicationContext())
                            {
                                db.Tariffs.Delete(tariff);
                                db.SaveChanges();
                                Tariffs = db.Tariffs.ReadAll();
                            }
                        }
                    },
                    (obj) => Tariffs.Count > 0)
                    );
            }
        }
    }
}
