using MVVM_Antipub.Models;
using MVVM_Antipub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace MVVM_Antipub.Views
{
    /// <summary>
    /// Логика взаимодействия для NewNote.xaml
    /// </summary>
    public partial class NewNoteWindow : Window
    {
        public NewNoteWindow(MainWindowViewModel parentViewModel)
        {
            NewNoteViewModel newNoteViewModel = new NewNoteViewModel(parentViewModel);
            newNoteViewModel.WANNACLOSE += Close;
            DataContext = newNoteViewModel;
            InitializeComponent();
        }
    }
}
