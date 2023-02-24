using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    public class Jour
    {
        public int Id { get; set; }


        public long PatientId { get; set; }
        public virtual PatientC Patient { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateJour { get; set; }

        public double[] VitesseMarcheMoyenne { get; set; }

        public int? TempsTotalActivite { get; set; }

        [DefaultValue(0)]
        public int? TempsAllongeTotal { get; set; } = 0;

        public bool JourTraite { get; set; }

        public int? NbPas { get; set; }


    }
}
