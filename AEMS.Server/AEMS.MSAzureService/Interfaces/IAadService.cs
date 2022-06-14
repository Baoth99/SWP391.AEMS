using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.MSAzureService
{
    public interface IAadService
    {
        Task<List<MSUserProfile>> GetUsers();

        Task<PowerBITokenModel> GetPowerBIToken(Guid reportId);
    }
}
