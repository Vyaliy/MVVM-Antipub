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
    /// Логика взаимодействия для TariffChange.xaml
    /// </summary>
    public partial class TariffWindow : Window
    {
        public TariffWindow(MainWindowViewModel mainWindowViewModel)
        {
            TariffWindowViewModel tariffChangeViewModel= new TariffWindowViewModel(mainWindowViewModel);
            tariffChangeViewModel.WANNACLOSE += Close;
            InitializeComponent();
            DataContext = tariffChangeViewModel;
        }
    }
}
