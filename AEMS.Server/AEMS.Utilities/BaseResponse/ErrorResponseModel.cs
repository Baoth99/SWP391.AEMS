using Newtonsoft.Json;

namespace AEMS.Utilities
{
    public class ErrorResponseModel
    {
        public int StatusCode { get; set; }

        public string MessageCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
