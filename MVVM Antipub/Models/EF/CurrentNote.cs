using MVVM_Antipub.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVM_Antipub.ViewModels
{

    public class CurrentNote : ClosedNote, INotifyPropertyChanged
    {
        public string Name { get; set; } = "Гость";
        public Tariff Tariff { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public new TimeSpan PastTime
        {
            get
            {
                return pastTime;
            }
            set
            {
                pastTime = value;
                OnPropertyChanged("PastTime");
            }
        }
        public new int Summ
        {
            get
            {
                return summ;
            }
            set
            {
                summ = value;
                OnPropertyChanged("Summ");
            }
        }
    }
}