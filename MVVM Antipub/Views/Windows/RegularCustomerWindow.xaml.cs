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
    /// Логика взаимодействия для RegularCustomerWindow.xaml
    /// </summary>
    public partial class RegularCustomerWindow : Window
    {
        private readonly MainWindowViewModel _parentViewModel;
        public RegularCustomerWindow(MainWindowViewModel parentWindowViewModel)
        {
            InitializeComponent();

            _parentViewModel = parentWindowViewModel;

            // Получаем RegularCustomerViewModel через DI
            var regularCustomerViewModel = AppHost.GetService<RegularCustomerWindowViewModel>();
            regularCustomerViewModel.Initialize(_parentViewModel); // Передаем в метод
            regularCustomerViewModel.WANNACLOSE += Close;

            DataContext = regularCustomerViewModel;
        }
    }
}