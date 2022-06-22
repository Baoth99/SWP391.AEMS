using AEMS.Utilities;
using System;
using System.Collections.Generic;

namespace AEMS.Application
{
    public class UpdateDeviceCommand : ICommand<BaseApiResponseModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Photos { get; set; }
    }
}
