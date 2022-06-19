using System;

namespace AEMS.MSAzureService
{
    public class PowerBITokenModel
    {
        public string Id { get; set; }

        public string EmmbedToken { get; set; }

        public string EmmbedUrl { get; set; }

        public string Type { get; set; }

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
