using AutoMapper;
using Workspace.DAL;
using Workspace.Entities;

namespace Workspace.BLL.Logic;

public class WorkspaceUserProfile : Profile
{
    public WorkspaceUserProfile()
    {
        //маппинг из базы данных в объект
        CreateMap<WorkspaceUserDTO, WorkspaceUser>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Login, src => src.MapFrom(x => x.Login))
            .ForMember(dest => dest.Password, src => src.MapFrom(x => x.Password))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Surname, src => src.MapFrom(x => x.Surname));

        //маппинг из объекта в сервис
        CreateMap<WorkspaceUser, WorkspaceUserResponse>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Login,
                src => src.MapFrom(x => x.Login))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Surname,
                src => src.MapFrom(x => x.Surname));

        //маппинг из сервиса в DTO
        CreateMap<WorkspaceUserDTO, WorkspaceUserResponse>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Login,
                src => src.MapFrom(x => x.Login))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Surname,
                src => src.MapFrom(x => x.Surname));

        //из Request в объект
        CreateMap<WorkspaceUserRequest, WorkspaceUser>()
            .ForMember(dest => dest.Login,
                src => src.MapFrom(x => x.Login))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Surname,
                src => src.MapFrom(x => x.Surname));

        //из объекта в DTO
        CreateMap<WorkspaceUser, WorkspaceUserDTO>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Login,
                src => src.MapFrom(x => x.Login))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Surname,
                src => src.MapFrom(x => x.Surname));
    }
}
