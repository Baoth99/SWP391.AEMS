using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AEMS.MSAzureService
{
    public class AadService : IAadService
    {
        private readonly IConfidentialClientApplication _confidentialClientApplication;

        private readonly string[] Scopes = new string[] { "https://graph.microsoft.com/.default" };
    
        public AadService(IConfidentialClientApplication confidentialClientApplication)
        {
            _confidentialClientApplication = confidentialClientApplication;
        }

        private GraphServiceClient GetGraphServiceClient()
        {
            var deletgate = new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                var authResult = await _confidentialClientApplication.AcquireTokenForClient(Scopes).ExecuteAsync();
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            });
            return new GraphServiceClient(deletgate);
        }

        public async Task<List<MSUserProfile>> GetUsers()
        {
            var graphServiceClient = GetGraphServiceClient();
            var aadUsers = await graphServiceClient.Users.Request().GetAsync();

            var userRes = aadUsers.Select(x => new MSUserProfile()
            {
                Id = Guid.Parse(x.Id),
                Name = x.DisplayName,
                Email = x.Mail,
                Phone = x.MobilePhone
            }).ToList();

            return userRes;
        }
    }
}
