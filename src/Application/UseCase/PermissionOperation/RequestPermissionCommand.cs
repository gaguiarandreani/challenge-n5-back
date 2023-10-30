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
    public class RequestPermissionCommand : IRequest<IAppResponseBase>
    {
        public PermissionDto Permission { get; set; }
    }

    public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, IAppResponseBase>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public RequestPermissionCommandHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        public async Task<IAppResponseBase> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            return await _permissionService.RequestPermission(_mapper.Map<PermissionRequest>(request));
        }
    }
}