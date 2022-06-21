using System;
using System.Collections.Generic;
using System.Text;

namespace AEMS.Monitors
{
    public class DeviceInfoDocument
    {
        public string Id { get; set; }

        public string DeviceId { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public string PowerBiDashboardId { get; set; }

        public string PowerBiDashboardEndpoint { get; set; }
    }
}
