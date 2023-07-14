using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.ViewModels
{
    class CurrentNote : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int CardNumber { get; set; }
        public DateTime ArrivalTime { get; set; }

        private TimeSpan pastTime;

        public TimeSpan PastTime
        {
            get
            {
                return pastTime;
            }
            set
            {
                pastTime = value;
                OnPropertyChanged("SetPastTime");
            }
        }

        public string TariffName { get; set; }
        public int Summ { get; set; }
        public string Comment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        

    }
}
