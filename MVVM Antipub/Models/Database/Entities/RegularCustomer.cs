using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.Models.Database
{
    public class RegularCustomer
    {
        private ICollection<ClosedNote> closedNotes;

        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }

        [Index(IsUnique = true)]
        
        public int CardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Comment { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Tariff Tariff { get; set; }
        [ForeignKey("Tariff")]
        public int TariffId { get; set; }
        public ICollection<ClosedNote> ClosedNotes 
        { 
            get => closedNotes;
            set
            {
                closedNotes = value;
            }
        }
    }
}
