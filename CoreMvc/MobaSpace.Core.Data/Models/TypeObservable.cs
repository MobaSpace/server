using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("TypeObservable")]
    public class TypeObservable
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string URI { get; set; }
    }
}
