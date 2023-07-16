using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using MVVM_Antipub.Models.Repositories;
using MVVM_Antipub.Views;
using MVVM_Antipub.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MVVM_Antipub.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        //Создание коллекции
        IDialogService dialogService;
        IFileService fileService;

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
                OnPropertyChanged("CurrentNotes");
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand = new RelayCommand(obj =>
                    {
                        //CurrentNote newCN = new CurrentNote() { Name = "ГостьДобавленный", CardNumber = 1, ArrivalTime = DateTime.Now, TariffName = "Стандарт" };
                        //CurrentNotes.Add(newCN);
                        //SelectedNote = newCN;
                        NewNote wndNewNote = new NewNote(this);
                        wndNewNote.ShowDialog();
                        SaveToFile(); //КОСТЫЛЬ
                    });
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        CurrentNote cn = obj as CurrentNote;
                        if (cn != null)
                        {
                            CurrentNotes.Remove(cn);
                            SaveToFile(); //ВОЗМОЖНО КОСТЫЛЬ
                        }
                    },
                    (obj) => CurrentNotes.Count > 0));
            }
        }
        private CurrentNote selectedNote;
        public CurrentNote SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
            }
        }


        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, CurrentNotes.ToList());
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var cn = fileService.Open(dialogService.FilePath);
                              CurrentNotes.Clear();
                              foreach (var p in cn)
                                  CurrentNotes.Add(p);
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
        private RelayCommand dBShow;
        public RelayCommand DBShow
        {
            get
            {
                return dBShow = new RelayCommand(obj =>
                {
                    DBShowWindow dBShowWindow = new DBShowWindow();
                    dBShowWindow.ShowDialog();
                });
            }
        }
        private RelayCommand tariffsShow;
        public RelayCommand TariffsShow
        {
            get
            {
                return tariffsShow = new RelayCommand(obj =>
                {
                    TariffChange tariffChange = new TariffChange(this);
                    tariffChange.ShowDialog();
                });
            }
        }
        public RelayCommand endNote;
        public RelayCommand EndNote
        {
            get
            {
                return endNote ??
                    (endNote = new RelayCommand(obj =>
                    {
                        
                        CurrentNote cn = (CurrentNote)obj;
                        ClosedNote closedNote = new ClosedNote() {ArrivalTime = cn.ArrivalTime, CardNumber = cn.CardNumber, Comment = cn.Comment, PastTime = cn.PastTime, Summ = cn.Summ, TariffNumber = 1, ShiftNumber = 1};
                        
                        if (closedNote != null)
                        {
                            using (var db = new ApplicationContext())
                            {
                                db.ClosedNotes.Add(closedNote);
                                db.SaveChanges();
                            }
                        }
                    },
                    (obj) => CurrentNotes.Count > 0));
            }
        }
        /// <summary>
        /// Основная VM с автоматической загрузкой данных из файла
        /// </summary>
        public MainWindowViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            this.dialogService = new DefaultDialogService();
            this.dialogService.FilePath = Path.Combine(Directory.GetCurrentDirectory(), "CurentNotes.json");
            if (!File.Exists(dialogService.FilePath))
            {
                File.Create(dialogService.FilePath);
            }
            CurrentNotes = new ObservableCollection<CurrentNote>();
            //FillTheNotes(currentNotes);
            OpenFromFile();
            SetTimer();
        }
        /// <summary>
        /// Откровенный костыль
        /// </summary>
        public void SaveToFile()
        {
            try
            {
                fileService.Save(dialogService.FilePath, CurrentNotes.ToList());
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }
        public void OpenFromFile()
        {
            try
            {
                var cn = fileService.Open(this.dialogService.FilePath);
                CurrentNotes.Clear();
                foreach (var p in cn)
                    CurrentNotes.Add(p);
                timer_Tick(new object(), new EventArgs()); //КОСТЫЛЬ
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }
        /// <summary>
        /// Метод для заполнения коллекции CurrentNote заранее заданными величинами
        /// </summary>
        public void FillTheNotes(ObservableCollection<CurrentNote> cn)
        {
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 1, ArrivalTime = DateTime.Now, TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 2, ArrivalTime = DateTime.Now, TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 3, ArrivalTime = DateTime.Now, TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 4, ArrivalTime = DateTime.Now, TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 5, ArrivalTime = DateTime.Now, TariffName = "Стандарт" });
            cn.Add(new CurrentNote() { Name = "Гость", CardNumber = 6, ArrivalTime = DateTime.Now, TariffName = "Стандарт" });
        }
        /// <summary>
        /// Метод для подстановки указанного аргумента в EventHandler
        /// </summary>
        
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

        private void timer_Tick(object sender, EventArgs e) //Оптимизировать, отделив обновление данных конкретно при открытии
        {
            //if (timer.IsEnabled)
            //    timer.Stop();
            for (int i = 0; i < currentNotes.Count; i++)
            {
                newone = DateTime.Now - currentNotes[i].ArrivalTime;
                currentNotes[i].PastTime = newone;
                currentNotes[i].Summ = 2 * Convert.ToInt32(Math.Truncate(currentNotes[i].PastTime.TotalMinutes));
            }
            //SaveToFile(); //КОСТЫЛЬ И ОЧЕНЬ ПЛОХОЙ
            //timer.Start();
        }
    }
}
