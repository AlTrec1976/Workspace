using AutoMapper;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            #region Маппинг Role
            //маппинг из RoleRequest в Role
            CreateMap<RoleRequest, Role>()
             .ForMember(dest => dest.RoleName, src => src.MapFrom(x => x.RoleName));
            //маппинг из Role в RoleDTO
            CreateMap<Role,RoleDTO>()
                .ForMember(dest => dest.RoleId, src=>src.MapFrom(x => x.RoleId))
                .ForMember(dest => dest.RoleName, src => src.MapFrom(x => x.RoleName));
            //маппинг из RoleDTO в RoleResponse
            CreateMap<RoleDTO, RoleResponse>()
                .ForMember(dest => dest.RoleId, src => src.MapFrom(x => x.RoleId))
                .ForMember(dest => dest.RoleName, src => src.MapFrom(x => x.RoleName));
            //маппинг из RoleResponse в Role
            CreateMap<RoleResponse, Role>()
                .ForMember(dest => dest.RoleId, src => src.MapFrom(x => x.RoleId))
                .ForMember(dest => dest.RoleName, src => src.MapFrom(x => x.RoleName));
            #endregion

            #region Маппинг Permission
            
            CreateMap<WorkspacePermissionRequest, WorkspacePermission>()
             .ForMember(dest => dest.PermissionName, src => src.MapFrom(x => x.PermissionName));
            
            CreateMap<WorkspacePermission, WorkspacePermissionDTO>()
                .ForMember(dest => dest.PermissionId, src => src.MapFrom(x => x.PermissionId))
                .ForMember(dest => dest.PermissionName, src => src.MapFrom(x => x.PermissionName));
            
            CreateMap<WorkspacePermissionDTO, WorkspacePermissionResponse>()
                .ForMember(dest => dest.PermissionId, src => src.MapFrom(x => x.PermissionId))
                .ForMember(dest => dest.PermissionName, src => src.MapFrom(x => x.PermissionName));
            
            CreateMap<WorkspacePermissionResponse, WorkspacePermission>()
                .ForMember(dest => dest.PermissionId, src => src.MapFrom(x => x.PermissionId))
                .ForMember(dest => dest.PermissionName, src => src.MapFrom(x => x.PermissionName));
            #endregion

            CreateMap<RolePermissionRequest, RolePermissionDTO>()
                .ForMember(dest => dest.RoleId, src => src.MapFrom(x => x.RoleId))
                .ForMember(dest => dest.PermissionId, src => src.MapFrom(x => x.PermissionId));

        }
    }
}
