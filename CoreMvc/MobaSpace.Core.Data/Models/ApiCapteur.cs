using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobaSpace.Core.Data.Models
{

    [Table("ApisCapteurs")]
    public class ApiCapteur
    {
        public long Id { get; set; }
        [Required]
        public virtual OAuth2Api Api { get; set; }
        [Required]
        public virtual Capteur Capteur { get; set; }
    }
}