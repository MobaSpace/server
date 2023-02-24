using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobaSpace.Web.UI.Models
{
    public class ApiSettings
    {
        public string Provider { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
