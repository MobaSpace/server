using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("CheckLists")]
    public class CheckList
    {
        public long Id { get; set; }
        public virtual PatientC Patient { get; set; }
        public string Check_Item { get; set; }
        [Column(TypeName = "Periodes[3]")]
        public Periodes programme { get; set; }
        [Column(TypeName = "smallint")]
        public int Rang { get; set; }
        [NotMapped]
        public virtual Patient UnencriptedPatient { get; set; }

    }

    public enum Periodes
    {
        matin,
        midi,
        soir
    }
}
