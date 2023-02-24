using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobaSpace.Core.Data.Models
{
    [Table("Capteurs")]
    public partial class Capteur
    {
        public long Id { get; set; }
        [Required]
        public string Marque { get; set; }
        [Required]
        [DisplayName("Modèle")]
        public string Modele { get; set; }
        [Required]
        public string Type { get; set; }
        [DisplayName("Désignation")]
        public string Designation => this.Marque + " " + this.Modele;
        public string DesignationComplete => this.Designation + (string.IsNullOrEmpty(this.Type) ? "" : $" ({this.Type})");
        [DisplayName("Etat")]
        public bool EtatOK { get; set; }
        public string Identifiant { get; set; }
        public DateTime Creation { get; set; }
        public DateTime CreationLocal => Creation.ToLocalTime();
        public DateTime Modification { get; set; }
        public DateTime ModificationLocal => Modification.ToLocalTime();

        public string CheminImage => $"/images/capteurs/{this.Designation}.png";

        public virtual ICollection<ApiCapteur> ApisCapteurs { get; set; } = new HashSet<ApiCapteur>();

        public override string ToString()
        {
            return $"{Designation} ({Identifiant})";
        }
    }
}
