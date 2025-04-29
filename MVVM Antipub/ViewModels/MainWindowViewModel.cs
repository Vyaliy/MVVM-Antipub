using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using MVVM_Antipub.Models.Database.Entities;
using MVVM_Antipub.Models.Repositories;
using MVVM_Antipub.Views;
using MVVM_Antipub.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using ApplicationContext = MVVM_Antipub.Models.ApplicationContext;

namespace MVVM_Antipub.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public void Initialize()
        {
            this.fileService = new JsonFileService();
            this.dialogService = new DefaultDialogService();
            this.dialogService.FilePath = Path.Combine(Directory.GetCurrentDirectory(), "CurentNotes.json");
            if (!File.Exists(dialogService.FilePath))
            {
                File.Create(dialogService.FilePath);
            }
            CurrentNotes = new ObservableCollection<CurrentNote>();
            using (var db = new ApplicationContext())
            {
                db.Shifts.Load();
                Sh = db.Shifts.Local.LastOrDefault(opened => opened.CloseTime == null);
            }
            OpenFromFile();
            SetTimer();
        }
        //Создание коллекции
        IDialogService dialogService;
        IFileService fileService;
        private Shift sh;
        public Shift Sh
        {
            get
            {
                return sh;
            }
            set
            {
                sh = value;
            }
        }
        private string currentShiftState;
        public string CurrentShiftState
        {
            get
            {
                using (var db = new ApplicationContext())
                {
                    db.Shifts.Load();
                    if (db.Shifts.Local.Any(opened => opened.CloseTime == null))
                    {
                        return
                            "Смена №" +
                            db.Shifts.Local.Last(opened => opened.CloseTime == null).Id.ToString();
                    }
                    return "Смена закрыта";
                }
            }
            set
            {
                currentShiftState = value;
                OnPropertyChanged("CurrentShiftState");
            }
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
                    using (var db = new ApplicationContext())
                    {
                        db.Shifts.Load();
                        if (db.Shifts.Local.LastOrDefault(opened => opened.CloseTime == null) == null)
                        {
                            dialogService.ShowMessage("Смена закрыта");
                            return;
                        }
                        if (db.Tariffs.Count() == 0)
                        {
                            dialogService.ShowMessage("Нет доступных тарифов");
                            return;
                        }
                    }
                    NewNoteWindow wndNewNote = new NewNoteWindow(this);
                    wndNewNote.ShowDialog();
                    SaveToFile(); //КОСТЫЛЬ
                }, (obj) => Sh != null);
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    RemoveFromFile();
            }
        }

        private RelayCommand RemoveFromFile()
        {
            return (removeCommand = new RelayCommand(obj =>
            {
                CurrentNote cn = obj as CurrentNote;
                if (cn != null)
                {
                    CurrentNotes.Remove(cn);
                    SaveToFile(); //ВОЗМОЖНО КОСТЫЛЬ
                }
            },
            (obj) => CurrentNotes.Count > 0 && SelectedNote is not null));
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
                    ClosedNotesShowWindow dBShowWindow = new ClosedNotesShowWindow();
                    dBShowWindow.ShowDialog();
                });
            }
        }
        private RelayCommand openShift;
        public RelayCommand OpenShift
        {
            get
            {
                return openShift = new RelayCommand(obj =>
                {
                    using (var db = new ApplicationContext())
                    {
                        db.Shifts.Load();
                        if (!db.Shifts.Local.Any(opened => opened.CloseTime == null))
                        {
                            Sh = new Shift();
                            db.Shifts.Add(Sh);
                            db.SaveChanges();
                            dialogService.ShowMessage($"Смена №{Sh.Id} успешно открыта");
                        }
                        else
                            dialogService.ShowMessage("Необходимо закрыть смену для открытия новой");
                    }
                    OnPropertyChanged("CurrentShiftState");
                });
            }
        }
        private RelayCommand closeShift;
        public RelayCommand CloseShift
        {
            get
            {
                return closeShift = new RelayCommand(obj =>
                {
                    if (CurrentNotes.Count > 0)
                    {
                        dialogService.ShowMessage("Невозможно закрыть смену с незакрытыми записями");
                        return;
                    }
                    using (var db = new ApplicationContext())
                    {
                        db.Shifts.Load();
                        if (db.Shifts.Local.Any(opened => opened.CloseTime == null))
                        {
                            Sh = db.Shifts.Local.Last(opened => opened.CloseTime == null);
                            //db.Shifts.Remove(sh);
                            Sh.CloseTime = DateTime.Now;
                            db.Shifts.Update(Sh);
                            db.SaveChanges();
                            dialogService.ShowMessage($"Смена №{Sh.Id} успешно закрыта");
                            Sh = null;
                        }
                        else
                            dialogService.ShowMessage("Нет открытой смены");

                    }
                    OnPropertyChanged("CurrentShiftState");

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
                    TariffWindow tariffChange = new TariffWindow(this);
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
                        ClosedNote closedNote = new ClosedNote();
                        closedNote.Id = cn.Id;
                        closedNote.TariffId = cn.TariffId;
                        closedNote.ShiftId = cn.ShiftId;
                        closedNote.CardNumber = cn.CardNumber;
                        closedNote.ArrivalTime = cn.ArrivalTime;
                        closedNote.PastTime = cn.PastTime;
                        closedNote.Summ = cn.Summ;


                        if (closedNote != null)
                        {
                            using (var db = new ApplicationContext())
                            {
                                var regular = db.RegularCustomers.FirstOrDefault(r => r.CardNumber == cn.CardNumber);
                                if (regular != null)
                                    closedNote.RegularCustomerId = regular.Id;

                                db.ClosedNotes.Add(closedNote);
                                db.SaveChanges();
                            }
                            CurrentNotes.Remove(cn);
                            SaveToFile(); //ВОЗМОЖНО КОСТЫЛЬ
                        }


                    },
                    (obj) => CurrentNotes.Count > 0 && SelectedNote is not null));
            }
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
            int sum = 0;
            foreach (var note in CurrentNotes)
            {
                note.PastTime = DateTime.Now - note.ArrivalTime;
                if (note.PastTime.TotalHours > note.Tariff.Hours.Count)
                {
                    foreach (var hour in note.Tariff.Hours)
                    {
                        sum += hour.Cost;
                    }
                    double temp = note.PastTime.TotalHours - (double)note.Tariff.Hours.Count;
                    sum += Convert.ToInt32(Math.Truncate((double)(note.Tariff.Hours.First(x => x.NumberOfHour == note.Tariff.Hours.Max(x => x.NumberOfHour)).Cost) * temp));
                }
                else
                {
                    foreach (var hour in note.Tariff.Hours)
                    {
                        double remainingTime;
                        if (note.PastTime.TotalHours >= hour.NumberOfHour)
                        {
                            sum += hour.Cost;
                        }
                        else
                        {
                            remainingTime = hour.NumberOfHour - note.PastTime.TotalHours;
                            if (remainingTime <= 1)
                                sum += Convert.ToInt32(Math.Truncate(hour.Cost * (1 - remainingTime)));
                        }
                    }
                }
                // Минимальная оплата
                if (note.Tariff.MinimumBill != null && sum < note.Tariff.MinimumBill)
                {
                    sum = note.Tariff.MinimumBill.Value;
                    note.Summ = sum;
                    return;
                }
                // Стоп-чек
                if (note.Tariff.StopCheck != null && sum > note.Tariff.StopCheck)
                {
                    sum = note.Tariff.StopCheck.Value;
                    note.Summ = sum;
                    return;
                }
                // Бесплатное время
                if (note.Tariff.FreeTimeMinutes != null && note.PastTime.TotalMinutes < note.Tariff.FreeTimeMinutes)
                {
                    sum = 0;
                    note.Summ = sum;
                    return;
                }
                note.Summ = sum;
                //sum += Convert.ToInt32(Math.Truncate(note.PastTime.TotalMinutes));
            }
            //SaveToFile(); //КОСТЫЛЬ И ОЧЕНЬ ПЛОХОЙ
            //timer.Start();
        }

        private RelayCommand regularCustomersShow;
        public RelayCommand RegularCustomersShow
        {
            get
            {
                return regularCustomersShow = new RelayCommand(obj =>
                {
                    RegularCustomerWindow regularCustomerWindow = new RegularCustomerWindow(this);
                    regularCustomerWindow.ShowDialog();
                });
            }
        }

        private RelayCommand currentClosedNotesShow;
        public RelayCommand CurrentClosedNotesShow
        {
            get
            {
                return currentClosedNotesShow = new RelayCommand(obj =>
                {
                    CurrentClosedNotesShowWindow currentClosedNotesShowWindow = new CurrentClosedNotesShowWindow();
                    currentClosedNotesShowWindow.ShowDialog();
                }, (obj) => Sh != null);
            }
        }
    }
}