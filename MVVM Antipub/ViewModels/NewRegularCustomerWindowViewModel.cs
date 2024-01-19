using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using MVVM_Antipub.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Antipub.ViewModels
{
    public class NewRegularCustomerWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Tariff> Tariffs { get; set; }
        public Tariff ChosenTariff { get; set; }

        public new RegularCustomerWindowViewModel parentViewModel { get; set; }
        public RegularCustomer RgCustomer { get; set; }
        public NewRegularCustomerWindowViewModel(RegularCustomerWindowViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;
            RgCustomer = new RegularCustomer();
            using (var db = new ApplicationContext())
            {
                //Tariffs = db.Tariffs.ReadAll();
                db.Tariffs.Load();
                Tariffs = db.Tariffs.Local.ToObservableCollection();
            }
        }
        public RelayCommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                {
                    using (var db = new Models.ApplicationContext())
                    {
                        RgCustomer.TariffId = ChosenTariff.Id;
                        RgCustomer.RegistrationDate = DateTime.Now;
                        
                        db.RegularCustomers.Load();
                        db.Tariffs.Load();
                        db.RegularCustomers.Local.Add(RgCustomer);
                        parentViewModel.RegularCustomers = db.RegularCustomers.Local.ToObservableCollection();
                        db.SaveChanges();
                    }
                    CloseThis();

                });
            }
        }
    }
}
