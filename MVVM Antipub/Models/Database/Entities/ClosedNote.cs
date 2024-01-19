using MVVM_Antipub.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVVM_Antipub.Models.Database
{
    public class ClosedNote
    {
        public int Id { get; set; }
        [ForeignKey("Tariff")]
        public int TariffId { get; set; }
        public Tariff Tariff { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }

        [ForeignKey("RegularCustomer")]
        public int? CardNumber { get; set; }
        public DateTime ArrivalTime { get; set; }
        protected TimeSpan pastTime;
        public TimeSpan PastTime
        {
            get;
            set;
        }
        protected int summ;
        public int Summ
        {
            get => summ;
            set
            {
                summ = value;
            }
        }
        public string Comment { get; set; }
        public ClosedNote()
        {

        }
    }
}
