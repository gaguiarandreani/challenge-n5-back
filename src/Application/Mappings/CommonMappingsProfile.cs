using AutoMapper;
using SecurityApi.Application.UseCase.PermissionOperation;
using SecurityApi.Domain.Dtos;
using SecurityApi.Domain.Entities;
using SecurityApi.Domain.Requests;

namespace SecurityApi.Application.Mappings
{
    public class CommonMappingsProfile : Profile
    {
        public CommonMappingsProfile()
        {
            CreateMap<GetPermissionsQuery, PermissionRequest>();
            CreateMap<ModifyPermissionCommand, PermissionRequest>();
            CreateMap<RequestPermissionCommand, PermissionRequest>();

            CreateMap<PermissionDto, Permission>();
            CreateMap<Permission, PermissionDto>();

            CreateMap<PermissionTypeDto, PermissionType>();
            CreateMap<PermissionType, PermissionTypeDto>();
        }
    }
}