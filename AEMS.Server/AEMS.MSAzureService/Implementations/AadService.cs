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
        private readonly string[] PowerBiScopes = new string[] { "https://analysis.windows.net/powerbi/api/.default" };

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

        private async Task<TokenCredentials> GetTokenCredentials()
        {
            var authResult = await _confidentialClientApplication.AcquireTokenForClient(PowerBiScopes).ExecuteAsync().ConfigureAwait(false);
            return new TokenCredentials(authResult.AccessToken, "Bearer");
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

        public async Task<PowerBITokenModel> GetPowerBIToken(Guid reportId)
        {
            var tokenCredencials = await GetTokenCredentials();

            using (var client = new PowerBIClient(new Uri(AppSettingValues.PowerBIApiUrl), tokenCredencials))
            {
                var report = new PowerBIReport();
                try
                {
                    var res = await client.Reports.GetReportInGroupWithHttpMessagesAsync(AppSettingValues.PowerBIGroupId, reportId);
                    report = JsonConvert.DeserializeObject<PowerBIReport>(await res.Response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    return PowerBITokenModel.Error(ex.Message);
                }


                var response = await client.Reports
                              .GenerateTokenInGroupWithHttpMessagesAsync(
                                    AppSettingValues.PowerBIGroupId,
                                    reportId,
                                    new GenerateTokenRequest(accessLevel: "view"));

                return new PowerBITokenModel()
                {
                    EmmbedToken = response.Body.Token,
                    EmmbedUrl = report.EmbedUrl,
                    ReportId = reportId
                };
            }
        }
    }
}
