using AEMS.DataAccess.DTOs;
using AEMS.Utilities;
using System.Threading.Tasks;

namespace AEMS.Application
{
    public interface IEquipmentService
    {
        Task<BaseApiResponseModel> GetList(GetEquipmentListQuery model);
    }
}
