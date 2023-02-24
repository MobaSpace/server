using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("Nuits")]
    public class NuitC
    {
        public int Id { get; set; }
        [Column(TypeName = "bigint")]
        public long key_id { get; set; }
        public byte[] nonce { get; set; }

        public DateTime DateNuit { get; set; }

        public long PatientId { get; set; }

        public byte[] DateDebut { get; set; }

        public byte[] DateFin { get; set; }

        public byte[] ScoreNuit { get; set; }

        public byte[] DureeSommeil { get; set; }

        public byte[] NbReveils { get; set; }

        public byte[] NbSorties { get; set; }

        public byte[] DureeReveilAuLit { get; set; }

        public byte[] DureeReveilHorsLit { get; set; }

        public byte[] DetailSorties { get; set; }

        public bool NuitTraitee { get; set; }

        
        public virtual PatientC Patient { get; set; }

        [NotMapped]
        public virtual Nuit UnencriptedNuit { get; set; }


    }
}
