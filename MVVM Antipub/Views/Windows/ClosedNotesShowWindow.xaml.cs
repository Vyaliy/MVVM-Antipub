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
    public partial class ClosedNotesShowWindow : Window
    {
        public ClosedNote ClosedNote { get; set; }
        public ClosedNotesShowWindow()
        {
            ClosedNotesShowWindowViewModel dBShowWindowViewModel = new ClosedNotesShowWindowViewModel();
            dBShowWindowViewModel.WANNACLOSE += Close;
            InitializeComponent();
            DataContext = dBShowWindowViewModel;
        }
    }
}