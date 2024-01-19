using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.Models.Database
{
    public class Hour
    {
        public int NumberOfHour { get; set; }
        public int Id { get; set; }
        public int Cost { get; set; }
        [ForeignKey("Tariff")]
        public int TariffId { get; set; }
    }
}