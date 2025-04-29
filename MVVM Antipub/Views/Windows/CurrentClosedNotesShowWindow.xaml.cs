using Microsoft.EntityFrameworkCore;
using MVVM_Antipub.Models;
using MVVM_Antipub.Models.Database;
using MVVM_Antipub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVM_Antipub.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для DBShow.xaml
    /// </summary>
    public partial class CurrentClosedNotesShowWindow : Window
    {
        public CurrentClosedNotesShowWindow()
        {
            InitializeComponent();

            // Получаем CurrentClosedNotesShowWindowViewModel через DI
            var currentClosedNotesShowWindowViewModel = AppHost.GetService<CurrentClosedNotesShowWindowViewModel>();
            currentClosedNotesShowWindowViewModel.Initialize();  // Передаём в метод
            currentClosedNotesShowWindowViewModel.WANNACLOSE += Close;

            DataContext = currentClosedNotesShowWindowViewModel;            
        }
    }
}