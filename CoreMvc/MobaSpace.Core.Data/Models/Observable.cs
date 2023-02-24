using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    public class Observable
    {
        public long Id { get; set; }

        [Column("Valeurs", TypeName = "jsonb")]
        public string Values { get; set; }
        [Column("TypeId")]
        public long TypeObservableId { get; set; }
        [Column(TypeName = "TimeStamp")]
        public DateTime Date { get; set; }
        public int Chambre { get; set; }
        public string UriPersonnel { get; set; }
        [DefaultValue(false)]
        public bool ObservableTraite { get; set; }
        [Column("Commentaire")]
        public string Commentaire { get; set; }
        [Column("PatId")]
        public long? PatientId { get; set; }
        public virtual PatientC Patient { get; set; }
        public virtual TypeObservable TypeObservable { get; set; }


    }
}
