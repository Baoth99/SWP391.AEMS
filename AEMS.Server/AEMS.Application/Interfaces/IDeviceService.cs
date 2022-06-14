using AEMS.DataAccess.DTOs;
using AEMS.Utilities;
using System.Threading.Tasks;

namespace AEMS.Application
{
    public interface IDeviceService
    {
        Task<BaseApiResponseModel> GetDevices(GetDeviceListQuery model);
    }
}
