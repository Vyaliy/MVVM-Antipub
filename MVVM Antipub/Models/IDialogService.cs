using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.ViewModels
{
    public interface IDialogService
    {
        public void ShowMessage(string message);   // показ сообщения
        public string FilePath { get; set; }   // путь к выбранному файлу
        public bool OpenFileDialog();  // открытие файла
        public bool SaveFileDialog();  // сохранение файла
    }
}
