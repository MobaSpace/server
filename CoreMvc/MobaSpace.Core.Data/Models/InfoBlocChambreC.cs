using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("InfoBlocChambre")]
    public class InfoBlocChambreC
    {
        public long Id { get; set; }

        [Column(TypeName = "bigint")]
        public long key_id { get; set; }
        public byte[] nonce { get; set; }

        public DateTime? Date { get; set; }
        [Required]
        public String UriNetSOINS { get; set; }

        public int? NumCh { get; set; }

        public byte[] Data { get; set; }

        public bool Traite { get; set; }
    }
}
