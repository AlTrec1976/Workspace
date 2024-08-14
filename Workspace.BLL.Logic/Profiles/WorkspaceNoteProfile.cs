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
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
            .ForMember(dest => dest.TaskId, src => src.MapFrom(x => x.TaskId));

        //маппинг из объекта в сервис
        CreateMap<WorkspaceNote, WorkspaceNoteResponse>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId))
            .ForMember(dest => dest.TaskId,
                src => src.MapFrom(x => x.TaskId));

        //маппинг из сервиса в DTO
        CreateMap<WorkspaceNoteDTO, WorkspaceNoteResponse>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId))
            .ForMember(dest => dest.TaskId,
                src => src.MapFrom(x => x.TaskId));

        //из Request в объект
        CreateMap<WorkspaceNoteRequest, WorkspaceNote>()
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId))
            .ForMember(dest => dest.TaskId,
                src => src.MapFrom(x => x.TaskId));

        //из объекта в DTO
        CreateMap<WorkspaceNote, WorkspaceNoteDTO>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Note,
                src => src.MapFrom(x => x.Note))
            .ForMember(dest => dest.UserId,
                src => src.MapFrom(x => x.UserId))
            .ForMember(dest => dest.TaskId,
                src => src.MapFrom(x => x.TaskId));
    }
}
