
using System;

namespace AEMS.MSAzureService
{
    public class PowerBITokenModel
    {
        public string EmmbedToken { get; set; }

        public string EmmbedUrl { get; set; }

        public Guid ReportId { get; set; }

        public string ErrorMessage { get; set; }


        public static PowerBITokenModel Error(string message)
        {
            return new PowerBITokenModel()
            {
                ErrorMessage = message
            };
        }
    }
}
