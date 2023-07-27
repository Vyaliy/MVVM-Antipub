using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Antipub.Models.Database
{
    public class RegularCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Index(IsUnique = true)]
        public int CardNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public int CurrentTariffNumber { get; set; }

    }
}
