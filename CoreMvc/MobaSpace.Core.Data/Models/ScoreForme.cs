using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("ScoreForme")]
    public class ScoreForme
    {

        public long Id { get; set; }

        public int NumCh { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(TypeName = "jsonb")]
        public String Current_values { get; set; }

        public float? TauxDeRemp { get; set; }

        public float? ScoreJour { get; set; }

        public float? ScorePred { get; set; }

        public float? IndiceDeConfiance { get; set; }

    }
}
