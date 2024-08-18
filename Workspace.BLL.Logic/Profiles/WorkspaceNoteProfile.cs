using AutoMapper;
using Workspace.Entities;

namespace Workspace.BLL.Logic;

public class WorkspaceNoteProfile : Profile
{
    public WorkspaceNoteProfile()
    {
        //маппинг из базы данных в объект
        CreateMap<WorkspaceNoteDTO, WorkspaceNote>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Note, src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId));

        //маппинг из объекта в сервис
        CreateMap<WorkspaceNote, WorkspaceNoteResponse>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId));

        //маппинг из сервиса в DTO
        CreateMap<WorkspaceNoteDTO, WorkspaceNoteResponse>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId));

        //из Request в объект
        CreateMap<WorkspaceNoteRequest, WorkspaceNote>()
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId));

        //из объекта в DTO
        CreateMap<WorkspaceNote, WorkspaceNoteDTO>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId));
    }
}
