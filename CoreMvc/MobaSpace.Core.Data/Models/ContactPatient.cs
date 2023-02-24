using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("ContactsPatients")]
    public class ContactPatient
    {
        public long Id { get; set; }
        [Required]
        public virtual User Utilisateur { get; set; }
        [Required]
        [ForeignKey("PatientId")]
        public virtual PatientC Patient { get; set; }
    }
}
