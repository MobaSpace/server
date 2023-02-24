using MobaSpace.Core.Data.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MobaSpace.Core.Data.Models
{
    [NotMapped]
    public class OAuth2ApiFitbit : OAuth2Api
    {
        public List<FitbitDevice> Devices { get; set; }
        public OAuth2ApiFitbit()
        {
            Provider = ApiType.Fitbit.ToString();
        }
    }

    [NotMapped]
    public class FitbitDevice : IApiDevice
    {
        [JsonIgnore]
        public static TimeSpan Timeout => TimeSpan.FromHours(1);
        public string battery { get; set; }
        public string deviceVersion { get; set; }
        public string id { get; set; }
        public string timezone { get; set; }
        public string lastSyncTime { get; set; }

        public string GetDeviceId() => id;
        public string GetDeviceModel() => deviceVersion;
        public string GetDeviceType() => "fitbit device";
        public DateTime GetLastSessionDate() => DateTime.Parse(lastSyncTime);

    }
}
