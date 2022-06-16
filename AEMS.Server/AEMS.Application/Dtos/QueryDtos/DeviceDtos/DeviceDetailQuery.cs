using System;
using System.Collections.Generic;

namespace AEMS.Application
{
    public class DeviceDetailQuery : IQuery<DeviceDetailViewModel>
    {
        public Guid Id { get; set; }
    }

    public class DeviceDetailViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Imei { get; set; }

        public string BusinessVersion { get; set; }

        public string FirmwareVersion { get; set; }

        public string ManagementVersion { get; set; }

        public string Description { get; set; }

        public string AreaCode { get; set; }

        public string AreaName { get; set; }

        public string Region { get; set; }

        public string CategoryName { get; set; }

        public string CategoryCode { get; set; }

        public PBITokenModel PowerBiEmbed { get; set; }

        public List<string> Photos { get; set; }

    }

    public class PBITokenModel
    {
        public Guid Id { get; set; }

        public string EmmbedToken { get; set; }

        public string EmmbedUrl { get; set; }

        public string Type { get; set; }

        public string ErrorMessage { get; set; }
    }
}
