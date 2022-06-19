using AEMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.Application
{
    public class CreateDeviceCommand : ICommand<BaseApiResponseModel>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Imei { get; set; }

        public string BusinessVersion { get; set; }

        public string FirmwareVersion { get; set; }

        public string ManagementVersion { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public Guid DeviceCategoryId { get; set; }

        public Guid AreaId { get; set; }
    }
}
