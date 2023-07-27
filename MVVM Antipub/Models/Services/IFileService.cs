using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.ViewModels
{
    public interface IFileService
    {
        List<CurrentNote> Open(string filename);
        void Save(string filename, List<CurrentNote> CurrentNotesList);
    }
}
