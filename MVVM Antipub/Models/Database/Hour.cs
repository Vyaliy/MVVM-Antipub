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
        public int NumberOfHour { get; set; } // Номер часа в тарифе
        public int Id { get; set; } // Внутренний ID. Первичный ключ.
        public int Cost { get; set; } // Стоимость часа
        [ForeignKey("TariffId")]
        public int TariffId { get; set; } // Внешний ключ на конкретный тариф
    }
}