using MobaSpace.Core.Data.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml;

namespace MobaSpace.Core.Data.Models
{
    [NotMapped]
    public class OAuth2ApiWithings : OAuth2Api
    {
        public List<WithingsDevice> Devices { get; set; }
        public OAuth2ApiWithings()
        {
            Provider = ApiType.Withings.ToString();
        }
    }

    [NotMapped]
    public class WithingsDevice : IApiDevice
    {
        public string type { get; set; }
        public string model { get; set; }
        public int model_id { get; set; }
        public string battery { get; set; }
        public string deviceid { get; set; }
        public string timezone { get; set; }
        public int last_session_date { get; set; }
        [JsonIgnore]
        public DateTime LastSessionDateUtc => DateTimeOffset.FromUnixTimeSeconds(last_session_date).DateTime;

        public string GetDeviceId() => deviceid;
        public string GetDeviceModel() => model;
        public string GetDeviceType() => type;
        public DateTime GetLastSessionDate() => LastSessionDateUtc;
    }
}
