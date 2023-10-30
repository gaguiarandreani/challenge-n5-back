using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Responses;
using SecurityApi.Domain.Requests;

namespace SecurityApi.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<AppResponseBase> RequestPermission(PermissionRequest request);

        Task<AppResponseBase> ModifyPermission(PermissionRequest request);

        Task<IAppResponseBase<List<PermissionDto>>> GetPermissions(PermissionRequest request);
    }
}