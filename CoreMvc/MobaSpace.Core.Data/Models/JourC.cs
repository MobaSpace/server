using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("Jours")]
    public class JourC
    {
        public int Id { get; set; }

        [Column(TypeName = "bigint")]
        public long key_id { get; set; }
        public byte[] nonce { get; set; }

        public long PatientId { get; set; }
        public virtual PatientC Patient { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateJour { get; set; }

        public byte[] VitesseMarcheMoyenne { get; set; }

        public byte[] TempsTotalActivite { get; set; }

        [DefaultValue(0)]
        public byte[] TempsAllongeTotal { get; set; }

        public byte[] NbPas { get; set; }

        public bool JourTraite { get; set; }

        [NotMapped]
        public virtual Jour UnencriptedJour { get; set; }



    }
}