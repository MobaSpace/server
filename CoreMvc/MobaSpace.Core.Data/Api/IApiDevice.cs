using System;
using System.Collections.Generic;
using System.Text;

namespace MobaSpace.Core.Data.Api
{
    public interface IApiDevice
    {
        public string GetDeviceId();

        public DateTime GetLastSessionDate();

        public string GetDeviceModel();

        public string GetDeviceType();

    }
}
