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
        public string Name { get; set; } // Имя тарифа
        public bool InUse { get; set; } // Используется ли сейчас тариф или же он помещен в "архив"
        public int? MinimumBill { get; set; } // Минимальная сумма к оплате. В случае, если сумма меньше этого значения, это значение будет суммой за посещение.
        public int? StopCheck { get; set; } // Стоп-чек. В случае если сумма больше этого значения, это значение будет суммой за посещение.
        public int? FreeTimeMinutes { get; set; } // Минимальное время для взятия оплаты. Количество минут бесплатного посещения.
        public string Comment { get; set; } // Комментарии к записи от администрации антикафе
        public List<Hour> Hours { get; set; } // Список с суммой каждого часа. Последний час в списке является окончательным и все последующие часы будут считаться по его стоимости
        public List<ClosedNote> ClosedNotes { get; set; } = new List<ClosedNote>(); // Нужно было писать комментарии заранее, т.к. теперь я не помню что это :D. Но наверно это что-то для БД.
        public Tariff(string tariffName, List<Hour> hours, int? StopCheck = null, int? MinCost = null, int? FreeTime = null)
        {
            Name = tariffName;
            Hours = hours;
            if (StopCheck != null)
                this.StopCheck = StopCheck;
            if (MinCost != null)
                this.MinimumBill = MinCost;
            if (FreeTime != null)
                this.FreeTimeMinutes = FreeTime;
            Comment = string.Empty;
            InUse = true;
        }
        public Tariff() { }
    }

}