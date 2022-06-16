using System;
using System.Threading.Tasks;

namespace AEMS.MSAzureService
{
    public interface IPowerBIService
    {
        Task<PowerBITokenModel> GetReportToken(Guid reportId);

        Task<PowerBITokenModel> GetDashboardToken(Guid dashboardId);
    }
}
