using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;
using MobaSpace.Core.Utils;

namespace MobaSpace.Core.Data.Models
{
    public class Alarme
    {
        public long Id { get; set; }

        public virtual Capteur Capteur { get; set; }
        public virtual PatientC Patient { get; set; }
        public virtual User Utilisateur { get; set; }
        [DisplayName("Priorité")]
        public int Priorite {get; set; }
        public string Description {get; set; }
        [DisplayName("Création")]
        public DateTime Creation { get; set; }
        public DateTime? Acquittement { get; set; }
        [DefaultValue(5)]
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
