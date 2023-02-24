using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [Table("OAuth2Apis")]
    public class OAuth2Api
    {
        public long Id { get; set; }
        public string ApiName { get; set; }
        [Required]
        public string Provider { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ApiUserId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        [DefaultValue(true)]
        public bool EtatOK { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientC Patient { get; set; }
        public virtual IEnumerable<ApiCapteur> ApiCapteurs { get; set; } = new HashSet<ApiCapteur>();

        [NotMapped]
        public virtual IEnumerable<Capteur> Capteurs
        {
            get
            {
                foreach (var ac in ApiCapteurs)
                {
                    yield return ac.Capteur;
                }
            }
        }

        public override string ToString()
        {
            return $"{Provider} linked to {Patient}";
        }

        public string CheminImage => $"/images/apis/{this.Provider?.Replace(" ", string.Empty)}.png";
    }

    public enum ApiType
    {
        Withings=0,
        Fitbit=1,
        SySPAD
    }
}
