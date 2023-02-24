using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MobaSpace.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MobaSpace.Core.Data.Models
{
    public class Patient : IValidatableObject
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Le numéro de chambre est obligatoire.")]
        [Display(Name="Nom Chambre/résident")]
        public string Chambre { get; set; }
        [Display(Name = "Numéro de la Chambre")]
        [Range(0, int.MaxValue, ErrorMessage = "La valeur doit être positif !")]
        public long? NumCh { get; set; }
        public string CheminPhoto { get; set; }

        public DateTime? DernierCoucher { get; set; }

        [DefaultValue(false)]
        public bool NouvellesDonneesLit { get; set; } = false;
        public DateTime? DernierLever { get; set; }

        [Column(TypeName = "jsonb")]
        public string DerniereLocalisation { get; set; }

        [Range(0, 23, ErrorMessage = "La valeur doit correspondre a un nombre entier !")]
        public int? Coucher_h { get; set; }
        [Range(0, 59, ErrorMessage = "La valeur doit correspondre a un nombre entier !")]
        public int? Coucher_min { get; set; }
        [Range(0, 23, ErrorMessage = "La valeur doit correspondre a un nombre entier !")]
        public int? Lever_h { get; set; }
        [Range(0, 59, ErrorMessage = "La valeur doit correspondre a un nombre entier !")]
        public int? Lever_min { get; set; }
        [DisplayName("Durée maximale hors du lit")]
        [Range(1, 120, ErrorMessage = "La valeur doit correspondre a un nombre entier !")]
        public int? DureeMaxHorsLit_min { get; set; }
        public POSTURE? Posture { get; set; }
        public int? TempsMaxAllongeJour { get; set;}

        public int? CumulTempsAllonge { get; set; }


        public override string ToString()
        {
            return $"{Chambre}";
        }

        public enum POSTURE 
        {
            Debout = 2,
            Assis = 1,
            Allonge = 0
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            #region Contrôle des couples partiellement renseignés
            HashSet<bool> valeursLit = new HashSet<bool>();
            valeursLit.Add(this.Coucher_h.HasValue);
            valeursLit.Add(this.Coucher_min.HasValue);
            valeursLit.Add(this.Lever_h.HasValue);
            valeursLit.Add(this.Lever_min.HasValue);
            valeursLit.Add(this.DureeMaxHorsLit_min.HasValue);
            if (valeursLit.Count > 1)
            {
                yield return new ValidationResult("Une des valeurs de coucher/lever/durée n'est pas renseignée");
            }
            #endregion


        }
    } 
}
