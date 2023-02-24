using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        public long Id { get; set; }
        public string URI { get; set; }
        public DateTime Date { get; set; }
        public string CodeRetour { get; set; }
        public string DetailRetour { get; set; }
    }
}
