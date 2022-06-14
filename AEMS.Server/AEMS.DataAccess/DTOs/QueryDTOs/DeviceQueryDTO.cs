using AEMS.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace AEMS.DataAccess.DTOs
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
}
