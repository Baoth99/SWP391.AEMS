using AEMS.Utilities;
using Microsoft.Graph;
using PowerBIReport = Microsoft.PowerBI.Api.Models.Report;
using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.PowerBI.Api.Models;

namespace AEMS.MSAzureService
{
    public class AadService : IAadService
    {
        private readonly IConfidentialClientApplication _confidentialClientApplication;

        private readonly string[] GraphScopes = new string[] { "https://graph.microsoft.com/.default"};
       

        public AadService(IConfidentialClientApplication confidentialClientApplication)
        {
            _confidentialClientApplication = confidentialClientApplication;
        }

        private GraphServiceClient GetGraphServiceClient()
        {
            var deletgate = new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                var authResult = await _confidentialClientApplication.AcquireTokenForClient(GraphScopes).ExecuteAsync().ConfigureAwait(false);
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
