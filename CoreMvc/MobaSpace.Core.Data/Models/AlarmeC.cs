using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;
using MobaSpace.Core.Utils;

namespace MobaSpace.Core.Data.Models
{
    [Table("Alarmes")]
    public class AlarmeC
    {
        public long Id { get; set; }

        [Column(TypeName = "bigint")]
        public long key_id { get; set; }
        public byte[] nonce { get; set; }

        public virtual Capteur Capteur { get; set; }
        public virtual PatientC Patient { get; set; }
        public virtual User Utilisateur { get; set; }
        public int Priorite { get; set; }
        public byte[] Description { get; set; }
        public DateTime Creation { get; set; }
        public DateTime? Acquittement { get; set; }
        public int NbNotifications { get; set; } = 5;
        public TimeSpan Duree => Acquittement.GetValueOrDefault(DateTime.Now) - Creation;
        public string DureeStr => Duree.ThereIs();
        public bool? Confirmation { get; set; }

        public String StringPriorite()
        {
            String StringPrio = "";
            if (this.Priorite >= 75)
            {
                StringPrio = "HAUTE";
            }
            else if (this.Priorite >= 50)
            {
                StringPrio = "MOYENNE";
            }
            else if (this.Priorite >= 25)
            {
                StringPrio = "BASSE";
            }
            else if (this.Priorite >= 0)
            {
                StringPrio = "AUCUNE";
            }
            return StringPrio;
        }
    }

}
