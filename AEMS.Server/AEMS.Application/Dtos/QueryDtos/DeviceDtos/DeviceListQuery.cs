using System;
using System.Collections.Generic;

namespace AEMS.Application
{
    public class GetDeviceListQuery : IQuery<IEnumerable<DeviceViewModel>>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public Guid? DeviceCategoryId { get; set; }

        public Guid? AreaId { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

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
