using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVM_Antipub.Models.Database
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool InUse { get; set; }
        public int MinimumBill { get; set; }
        public int StopCheck { get; set; }
        public int FreeTimeMinutes { get; set; }
        public string Comment { get; set; }
        public List<Hour> Hours { get; set; }
        public List<ClosedNote> ClosedNotes { get; set; } = new List<ClosedNote>();
        public Tariff(string tariffName, List<Hour> hours)
        {
            Name = tariffName;
            Hours = hours;
            StopCheck = 0;
            FreeTimeMinutes = 0;
            Comment = string.Empty;
            InUse = true;
            MinimumBill = 0;
        }
        public Tariff() { }
    }
    
}
