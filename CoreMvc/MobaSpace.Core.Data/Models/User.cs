using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Le champ {0} doit être rempli")]
        [Display(Name = "Identifiant")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Le champ {0} doit être rempli")]
        [Display(Name = "Prenom")]
        public string UserFirstname { get; set; }

        [Required(ErrorMessage = "Le champ {0} doit être rempli")]
        [Display(Name = "Nom")]
        public string UserSurname { get; set; }

        [EmailAddress(ErrorMessage = "L'adresse e-mail rentrée n'est pas valide.")]
        [Display(Name = "Email")]
        public override string Email { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?:(?:\\+|00)33|0|33)\\s*[1-9](?:[\\s.-]*\\d{2}){4}$", ErrorMessage = "Le format du numéro n'est pas valide")]
        [Display(Name = "Numero appel d'alarme")]
        public string Appel { get; set; }
        
        [DisplayName("Création")]
        public DateTime Creation { get; set; }
        
        [DisplayName("Last connection")]
        public DateTime LastConnection { get; set; }

        [DisplayName("UriNetSoins")]
        public string? UriNetSOINS { get; set; }

        [Display(Name = "Rattacher à NetSOINS")]
        public bool Linked2NetSOINS { get; set; }

        [Required(ErrorMessage = "Le champ {0} doit être rempli")]
        [StringLength(100, ErrorMessage = "Le {0} doit être au minimum de {2} et au maximum de {1} caractères.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        [NotMapped]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et la {0} ne correspondent pas.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Canal de Notification Android : ")]
        public string CanalNotif { get; set; }

        [DisplayName("Password inv")]
        public DateTime? DatePasswordInv { get; set; }
    }

    public static class Roles
    {
        public const string Soignant = "Soignant (AS + IDE)";
        public const string Infirmier = "Cadre de soins (IDEC)";
        public const string Administrateur = "Administrateur";


        public static IEnumerable<string> GetRoles()
        {
            return MobaSpace.
                Core.
                Reflexion.
                ReflexionTools.GetStaticsFields<string>(typeof(Roles));
        }
    }
}
