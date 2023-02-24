using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("Trackers")]
    public class TrackerC
    {
        public long Id { get; set; }

        public long key_id { get; set; }
        
        public byte[] nonce { get; set; }

        [Column(TypeName = "jsonb")]
        public string LecturesWifi { get; set; }
        
        public byte[] NbPas { get; set; }

        public Double[] AccVector { get; set; }

        public DateTime LastUpdate { get; set; }

        public byte[] VitesseMarche { get; set; }

        public byte[] ActivityTime { get; set; }

        [Required]
        public virtual Capteur Capteur { get; set; }

        public double Power { get; set; }

        public bool Traite { get; set; }
    }
}

