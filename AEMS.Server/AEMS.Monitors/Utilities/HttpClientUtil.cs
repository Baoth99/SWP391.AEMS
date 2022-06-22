using Amazon.Lambda.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.Monitors
{
    public class HttpClientUtil
    {
        public static async Task ClientPost(string url, string content)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, httpContent);
                if (result.IsSuccessStatusCode)
                {
                    LambdaLogger.Log($"Post Successfully !");
                }
            }
        }
    }
}
