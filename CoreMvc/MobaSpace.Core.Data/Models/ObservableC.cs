using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("Observables")]
    public class ObservableC
    {
        public long Id { get; set; }
        [Column(TypeName = "bigint")]
        public long key_id { get; set; }
        public byte[] nonce { get; set; }
        public byte[] Values { get; set; }
        public long TypeObservableId { get; set; }
        public DateTime Date { get; set; }
        public byte[] Chambre { get; set; }
        public string UriPersonnel { get; set; }
        public bool ObservableTraite { get; set; }
        public byte[] Commentaire { get; set; }
        public long? PatientId { get; set; }
        public virtual PatientC Patient { get; set; }
        public virtual TypeObservable TypeObservable { get; set; }


    }
}
