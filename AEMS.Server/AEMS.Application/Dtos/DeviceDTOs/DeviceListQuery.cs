using System;
using System.Collections.Generic;

namespace AEMS.Application
{
    public class GetDeviceListQuery : IQuery<IEnumerable<DeviceViewModel>>
    {
    }

    public class DeviceViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Imei { get; set; }

        public string BusinessVersion { get; set; }

        public string FirmwareVersion { get; set; }

        public string ManagementVersion { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }
}
