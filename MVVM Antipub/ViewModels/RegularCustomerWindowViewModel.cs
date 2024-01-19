using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using MVVM_Antipub.Models.Repositories;
using MVVM_Antipub.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.ViewModels
{
    public class RegularCustomerWindowViewModel : ViewModelBase
    {
        private ObservableCollection<RegularCustomer> regularCustomers;
        public ObservableCollection<RegularCustomer> RegularCustomers
        {
            get
            {
                return regularCustomers;
            }
            set
            {
                regularCustomers = value;
                OnPropertyChanged("RegularCustomers");
            }
        }
        private RegularCustomer selectedRegularCustomer;
        public RegularCustomer SelectedRegularCustomer
        {
            get { return selectedRegularCustomer; }
            set
            {
                selectedRegularCustomer = value;
                OnPropertyChanged("SelectedRegularCustomer");
            }
        }
        public RegularCustomerWindowViewModel(MainWindowViewModel parentViewModel)
        {
            base.parentViewModel = parentViewModel;
            using (var db = new ApplicationContext())
            {
                db.Tariffs.Load();
                db.RegularCustomers.Load();
                RegularCustomers = db.RegularCustomers.Local.ToObservableCollection();
            }
        }
        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                {
                    NewRegularCustomerWindow newRegularCustomerWindow = new NewRegularCustomerWindow(this);
                    newRegularCustomerWindow.ShowDialog();
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
                        RegularCustomer rgCust = obj as RegularCustomer;
                        if (rgCust != null)
                        {
                            using (var db = new ApplicationContext())
                            {
                                db.RegularCustomers.Remove(rgCust);
                                db.SaveChanges();
                                db.RegularCustomers.Load();
                                RegularCustomers = db.RegularCustomers.Local.ToObservableCollection();
                            }
                        }
                    },
                    (obj) => RegularCustomers.Count > 0)
                    );
            }
        }

    }
}
