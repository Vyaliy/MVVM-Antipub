using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVM_Antipub.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CurrentNote> vs;
        public ObservableCollection<CurrentNote> Vs
        {
            get
            {
                return vs;
            }
            set
            {
                vs = value;
                OnPropertyChanged();
            }
        }
        public void Test()
        {
            vs = new ObservableCollection<CurrentNote>();
            vs.Add(new CurrentNote() { Name = "Гость", CardNumber = 1, ArrivalTime = new DateTime(2021, 06, 30, 1, 25, 00), TariffName = "Стандарт", Summ = 1 });
            //Vs.Add(0);
        }
        private ObservableCollection<CurrentNote> currentNotes;

        public ObservableCollection<CurrentNote> CurrentNotes
        {
            get
            {
                return currentNotes;
            }

            set
            {
                currentNotes = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            currentNotes = new ObservableCollection<CurrentNote>();
            FillTheNotes(currentNotes);
            Test();
            SetTimer();
            Refresh();
        }

        public void FillTheNotes(ObservableCollection<CurrentNote> cn)
        {
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 1, ArrivalTime = new DateTime(2021, 06, 30, 1, 25, 00), TariffName = "Стандарт", Summ = 5 });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 2, ArrivalTime = new DateTime(2021, 06, 30, 1, 13, 00), TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 3, ArrivalTime = new DateTime(2021, 06, 30, 1, 12, 00), TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 4, ArrivalTime = new DateTime(2021, 06, 30, 1, 23, 00), TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 5, ArrivalTime = new DateTime(2021, 06, 30, 1, 15, 30), TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 6, ArrivalTime = new DateTime(2021, 06, 30, 1, 06, 44), TariffName = "Стандарт" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public DispatcherTimer timer = new DispatcherTimer();
        TimeSpan ts = new TimeSpan(0, 0, 1);
        TimeSpan newone = new TimeSpan();
        private void SetTimer()
        {
            timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = new TimeSpan(0, 0, 60 - DateTime.Now.Second);
            timer.Interval = ts;
            //this.currentNotes[2].PastTime = currentNotes[0].PastTime.Add(new TimeSpan(0, 1, 0));
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            //Refresh();
            Vs[0].Summ++;
            OnPropertyChanged();
            Vs.Add(new CurrentNote());
            Vs.RemoveAt(Vs.Count - 1);
            for (int i = 0; i < currentNotes.Count; i++)
            {
                newone = DateTime.Now - currentNotes[i].ArrivalTime;
                currentNotes[i].PastTime = newone;
                currentNotes[i].Summ = 2 * Convert.ToInt32(currentNotes[i].PastTime.TotalMinutes);
            }
            //КОСТЫЛЬ!!!
            CurrentNotes.Add(new CurrentNote());
            CurrentNotes.RemoveAt(CurrentNotes.Count - 1);
            timer.Start();
        }

        public ObservableCollection<CurrentNote> Refresh()
        {
            for (int i = 0; i < currentNotes.Count; i++)
            {
                newone = DateTime.Now - currentNotes[i].ArrivalTime;
                currentNotes[i].PastTime = newone;
                currentNotes[i].Summ = 2 * Convert.ToInt32(currentNotes[i].PastTime.TotalMinutes);
            }
            Vs[0].Summ++;
            //currentNotes.Add(new CurrentNote());
            //currentNotes.RemoveAt(currentNotes.Count-1);
            return currentNotes;
        }


    }
}
