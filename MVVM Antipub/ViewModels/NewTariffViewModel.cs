using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MVVM_Antipub.ViewModels
{
    public class NewTariffViewModel : ViewModelBase
    {
        
        public ObservableCollection<Hour> Hours { get; set; }
        public Tariff Tariff { get; set; }
        public string Name { get; set; }
        public new TariffChangeViewModel parentViewModel { get; set; }
        public NewTariffViewModel(TariffChangeViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;
            Hours = new ObservableCollection<Hour>
            {
                new Hour() {NumberOfHour = 1, Cost = 100}
            };
        }
        private RelayCommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                {
                    Tariff = new Tariff(Name, Hours.ToList());
                    Tariff.Name = Name;
                    parentViewModel.Tariffs.Add(Tariff);
                    CloseThis();

                });
            }
        }
    }
}
