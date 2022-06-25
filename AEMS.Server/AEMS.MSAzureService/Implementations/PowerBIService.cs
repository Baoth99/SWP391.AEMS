using AEMS.Utilities;
using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PowerBIReport = Microsoft.PowerBI.Api.Models.Report;

namespace AEMS.MSAzureService
{
    public class PowerBIService : IPowerBIService
    {
        private readonly string[] PowerBiScopes = new string[] { "https://analysis.windows.net/powerbi/api/.default" };

        private readonly IConfidentialClientApplication _confidentialClientApplication;

        public PowerBIService(IConfidentialClientApplication confidentialClientApplication)
        {
            _confidentialClientApplication = confidentialClientApplication;
        }


        private async Task<TokenCredentials> GetTokenCredentials()
        {
            var authResult = await _confidentialClientApplication.AcquireTokenForClient(PowerBiScopes).ExecuteAsync().ConfigureAwait(false);
            return new TokenCredentials(authResult.AccessToken, "Bearer");
        }


        public async Task<PowerBITokenModel> GetDashboardToken(string dashboardId)
        {
            var tokenCredencials = await GetTokenCredentials();
            using (var client = new PowerBIClient(new Uri(AppSettingValues.PowerBIApiUrl), tokenCredencials))
            {
                try
                {
                    var groupId = Guid.Parse(AppSettingValues.PowerBIGroupId);
                    var dashboard = await client.Dashboards.GetDashboardInGroupAsync(groupId, Guid.Parse(dashboardId));

                    var tokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");

                    var response = await client.Dashboards.GenerateTokenInGroupAsync(groupId, Guid.Parse(dashboardId), tokenRequestParameters);

                    return new PowerBITokenModel()
                    {
                        Id = dashboardId,
                        Type = "dashboard",
                        EmmbedToken = response.Token,
                        EmmbedUrl = dashboard.EmbedUrl,
                    };
                }
                catch (Exception ex)
                {
                    return PowerBITokenModel.Error(ex.Message);
                }
            }
        }

        public async Task<PowerBITokenModel> GetReportToken(string reportId)
        {
            var tokenCredencials = await GetTokenCredentials();
            using (var client = new PowerBIClient(new Uri(AppSettingValues.PowerBIApiUrl), tokenCredencials))
            {
                try
                {
                    var groupId = Guid.Parse(AppSettingValues.PowerBIGroupId);
                    var report = await client.Reports.GetReportInGroupAsync(groupId, Guid.Parse(reportId));

                    var tokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");

                    var response = await client.Reports.GenerateTokenInGroupAsync(groupId, Guid.Parse(reportId), tokenRequestParameters);

                    return new PowerBITokenModel()
                    {
                        Id = reportId,
                        Type = "report",
                        EmmbedToken = response.Token,
                        EmmbedUrl = report.EmbedUrl,
                    };
                }
                catch (Exception ex)
                {
                    return PowerBITokenModel.Error(ex.Message);
                }
            }
        }
    }
}
