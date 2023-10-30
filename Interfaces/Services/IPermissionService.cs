using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Responses;
using SecurityApi.Domain.Requests;

namespace SecurityApi.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<IAppResponseBase<PermissionDto>> RequestPermission(PermissionRequest request);

        Task<IAppResponseBase<PermissionDto>> ModifyPermission(PermissionRequest request);

        Task<IAppResponseBase<List<PermissionDto>>> GetPermissions(PermissionRequest request);
    }
}