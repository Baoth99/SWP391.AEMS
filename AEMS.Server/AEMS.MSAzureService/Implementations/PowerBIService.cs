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


        public async Task<PowerBITokenModel> GetDashboardToken(Guid dashboardId)
        {
            var tokenCredencials = await GetTokenCredentials();
            using (var client = new PowerBIClient(new Uri(AppSettingValues.PowerBIApiUrl), tokenCredencials))
            {
                try
                {
                    var dashboardGuid = Guid.Parse(AppSettingValues.PowerBIGroupId);
                    var dashboard = await client.Dashboards.GetDashboardInGroupAsync(dashboardGuid, dashboardId);

                    var tokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");

                    var response = await client.Dashboards.GenerateTokenInGroupAsync(dashboardGuid, dashboardId, tokenRequestParameters);

                    return new PowerBITokenModel()
                    {
                        Id = dashboardId,
                        Type = "Report",
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

        public async Task<PowerBITokenModel> GetReportToken(Guid reportId)
        {
            var tokenCredencials = await GetTokenCredentials();
            using (var client = new PowerBIClient(new Uri(AppSettingValues.PowerBIApiUrl), tokenCredencials))
            {
                try
                {
                    var reportGuid = Guid.Parse(AppSettingValues.PowerBIGroupId);
                    var report = await client.Reports.GetReportInGroupAsync(reportGuid, reportId);

                    var tokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");

                    var response = await client.Reports.GenerateTokenInGroupAsync(reportGuid, reportId, tokenRequestParameters);

                    return new PowerBITokenModel()
                    {
                        Id = reportId,
                        Type = "Report",
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
