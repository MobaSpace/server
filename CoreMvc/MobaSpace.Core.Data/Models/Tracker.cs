using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    public class Tracker
    {
        public long Id { get; set; }

        [Column(TypeName = "jsonb")]
        public string LecturesWifi { get; set; }

        public long NbPas { get; set; }

        public Double[] AccVector { get; set; }

        public double VitesseMarche { get; set; }

        public int ActivityTime { get; set; }

        public DateTime LastUpdate { get; set; }

        [Required]
        public virtual Capteur Capteur { get; set; }

        public double Power { get; set; }

        public bool Traite { get; set; }
    }
}
