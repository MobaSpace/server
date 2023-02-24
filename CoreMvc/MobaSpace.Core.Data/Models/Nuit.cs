using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    public class Nuit
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateNuit { get;  set; }

        public long PatientId { get; set; }

        public DateTime? DateDebut { get;  set; }

        public DateTime? DateFin { get;  set; }

        public int? ScoreNuit { get; set; }

        public TimeSpan? DureeSommeil { get; set; }

        public int? NbReveils { get; set; }

        public int? NbSorties { get; set; }

        public TimeSpan? DureeReveilAuLit { get; set; }

        public TimeSpan? DureeReveilHorsLit { get; set;}

        [Column(TypeName = "jsonb")]
        public string DetailSorties { get; set; }
       
        public bool NuitTraitee { get; set; }




        public virtual PatientC Patient { get; set; }


    }
}
