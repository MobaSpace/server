using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    public class InfoBlocChambre
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public String UriNetSOINS { get; set; }

        public int NumCh { get; set; }

        [Column(TypeName = "jsonb")]
        public String Data { get; set; }

        public bool Traite { get; set; }
    }
}
