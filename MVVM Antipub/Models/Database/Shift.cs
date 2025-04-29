using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.Models.Database.Entities
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime OpenTime { get; set; } = DateTime.Now;
        public DateTime? CloseTime { get; set; }
    }
}