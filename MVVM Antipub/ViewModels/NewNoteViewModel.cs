using MVVM_Antipub.Models;
using MVVM_Antipub.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_Antipub.ViewModels
{
    public class NewNoteViewModel : ViewModelBase
    {
        private MainWindowViewModel _parentViewModel;

        public void Initialize(MainWindowViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
        }
        public string Name { get; set; }
        public int CardNumber { get; set; }
        public string TariffName { get; set; }
        public CurrentNote Cn { get; set; }
        public RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                {
                    Cn = new CurrentNote() { Name = Name, CardNumber = CardNumber, ArrivalTime = DateTime.Now, TariffName = TariffName };
                    if (Cn.Name == null || Cn.Name == "") Cn.Name = "Гость";
                    if (Cn.TariffName == null || Cn.TariffName == "") Cn.TariffName = "Стандартный";
                    _parentViewModel.CurrentNotes.Add(Cn);
                    CloseThis();
                    
                });
            }
        }

    }
}
