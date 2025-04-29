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

        public int CardNumber { get; set; } // Номер карты клиента
        public string? PhoneNumber { get; set; } // Номер телефона клиента
        public string? Comment { get; set; } // Комментарий к клиенту
        public DateTime RegistrationDate { get; set; } // Дата регистрации клиента в БД
        public Tariff Tariff { get; set; } // Тариф привязанный к этому клиенту
        [ForeignKey("Tariff")]
        public int TariffId { get; set; } // Внешний ключ к ID Тарифа для этого клиента
        public ICollection<ClosedNote> ClosedNotes // Хз что это. Видимо это для связки в БД с закрытыми записями, чтобы увидеть какие для него.
        {
            get => closedNotes;
            set
            {
                closedNotes = value;
            }
        }
    }
}