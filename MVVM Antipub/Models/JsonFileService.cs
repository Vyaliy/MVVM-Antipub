using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using MVVM_Antipub.ViewModels;
using System.Text.Json;

namespace MVVM_Antipub.Models
{
    public class JsonFileService : IFileService
    {
        public List<CurrentNote> Open(string filename)
        {
            List<CurrentNote> phones = new List<CurrentNote>();
            //DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<CurrentNote>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                //phones = jsonFormatter.ReadObject(fs) as List<CurrentNote>;
                if (fs.Length > 0)
                {
                    phones = JsonSerializer.Deserialize<List<CurrentNote>>(fs);
                }
            }
            return phones;
        }

        public void Save(string filename, List<CurrentNote> notesList)
        {
            //DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<CurrentNote>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                //jsonFormatter.WriteObject(fs, notesList);
                JsonSerializer.Serialize<List<CurrentNote>>(fs, notesList);
            }
        }
    }
}
