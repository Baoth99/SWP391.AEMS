using System;
using System.Collections.Generic;

namespace AEMS.Application
{
    public class DeviceCategoryListQuery : IQuery<IEnumerable<DeviceCategoryViewModel>>
    {
    }

    public class DeviceCategoryViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
