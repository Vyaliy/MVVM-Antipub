using MVVM_Antipub.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int TariffNumber { get; set; }
        public int ShiftNumber { get; set; }
        
        public int CardNumber { get; set; }
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

        public ClosedNote(int tariffNumber, int shiftNumber, int cardNumber, DateTime arrivalTime, TimeSpan pastTime, int summ, string comment = "")
        {
            TariffNumber = tariffNumber;
            ShiftNumber = shiftNumber;
            CardNumber = cardNumber;
            ArrivalTime = arrivalTime;
            PastTime = pastTime;
            Summ = summ;
            Comment = comment;
        }
        public ClosedNote(ClosedNote closedNote)
        {
            TariffNumber = closedNote.TariffNumber;
            ShiftNumber = closedNote.ShiftNumber;
            CardNumber = closedNote.CardNumber;
            ArrivalTime = closedNote.ArrivalTime;
            PastTime = closedNote.PastTime;
            Summ = closedNote.Summ;
            Comment = closedNote.Comment;
        }
        public ClosedNote()
        {

        }
    }
}
