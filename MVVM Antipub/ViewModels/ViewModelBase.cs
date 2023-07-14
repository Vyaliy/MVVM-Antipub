using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_Antipub.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase parentViewModel { get; set; }
        public event Action WANNACLOSE;
        public event PropertyChangedEventHandler PropertyChanged;

        public void CloseThis()
        {
            WANNACLOSE?.Invoke();
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
