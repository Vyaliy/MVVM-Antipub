﻿using MVVM_Antipub.ViewModels;
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
    /// Логика взаимодействия для NewTariffWindow.xaml
    /// </summary>
    public partial class NewTariffWindow : Window
    {
        public NewTariffWindow(TariffWindowViewModel parent)
        {
            NewTariffViewModel newTariffWindowViewModel = new NewTariffViewModel(parent);
            newTariffWindowViewModel.WANNACLOSE += Close;
            DataContext = newTariffWindowViewModel;
            InitializeComponent();
        }
    }
}
