using System;
using System.Threading.Tasks;

namespace AEMS.MSAzureService
{
    public interface IPowerBIService
    {
        Task<PowerBITokenModel> GetReportToken(string reportId);

        Task<PowerBITokenModel> GetDashboardToken(string dashboardId);
    }
}
