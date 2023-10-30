using AutoMapper;
using MediatR;
using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Responses;
using SecurityApi.Domain.Requests;
using SecurityApi.Interfaces.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityApi.Application.UseCase.PermissionOperation
{
    public class GetPermissionsQuery : IRequest<IAppResponseBase<List<PermissionDto>>>
    {
    }

    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IAppResponseBase<List<PermissionDto>>>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public GetPermissionsQueryHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        public async Task<IAppResponseBase<List<PermissionDto>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetPermissions(new PermissionRequest());
        }
    }
}