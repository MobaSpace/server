using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("Balances")]
    public class Balance
    {
        public long Id { get; set; }

        [Required]
        public string AdresseMAC { get; set; }

        public string Nom { get; set; }

        [Column(TypeName = "jsonb")]
        public string Lectures { get; set; }

        public DateTime DernierePesee { get; set; }
        
        public double Valeur { get; set; }
    }
}
