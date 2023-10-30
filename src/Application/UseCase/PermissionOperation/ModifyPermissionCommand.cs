using AutoMapper;
using MediatR;
using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Responses;
using SecurityApi.Domain.Requests;
using SecurityApi.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityApi.Application.UseCase.PermissionOperation
{
    public class ModifyPermissionCommand : IRequest<IAppResponseBase>
    {
        public PermissionDto Permission { get; set; }
    }

    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, IAppResponseBase>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public ModifyPermissionCommandHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        public async Task<IAppResponseBase> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.ModifyPermission(_mapper.Map<PermissionRequest>(request));
        }
    }
}