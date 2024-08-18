using AutoMapper;
using Workspace.DAL;
using Workspace.Entities;


namespace Workspace.BLL.Logic;

public class WorkspaceTaskProfile : Profile
{
    public WorkspaceTaskProfile()
    {
        //маппинг из базы данных в объект
        CreateMap<WorkspaceTaskDTO, WorkspaceTask>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Status, src => src.MapFrom((x) => StatusTask.FromValue(x.Status)))
            //.ForMember(dest => dest.Notes, src => src.MapFrom(x => x.Notes));
        .ForPath(dest => dest.Manager.Id, src => src.MapFrom(x => x.ManagerId))
        .ForPath(dest => dest.Employee.Id, src => src.MapFrom(x => x.EmployeeId));

        //маппинг из объекта в сервис
        CreateMap<WorkspaceTask, WorkspaceTaskResponse>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Status,
                src => src.MapFrom(x => x.Status.IdStatus))
            .ForMember(dest => dest.StatusName,
                src => src.MapFrom(x => x.Status.NameForStatus))
            //.ForMember(dest => dest.Notes,
            //    src => src.MapFrom(x => x.Notes));
            .ForPath(dest => dest.ManagerId,
                src => src.MapFrom(x => x.Manager.Id))
            .ForPath(dest => dest.EmployeeId,
                src => src.MapFrom(x => x.Employee.Id));

        //маппинг из DTO в сервиса
        CreateMap<WorkspaceTaskDTO, WorkspaceTaskResponse>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => 1))
            //.ForMember(dest => dest.Notes, 
            //    src => src.MapFrom(x => x.Notes))
            .ForPath(dest => dest.ManagerId, 
                src => src.MapFrom(x => x.ManagerId))
            .ForPath(dest => dest.EmployeeId, 
                src => src.MapFrom(x => x.EmployeeId));

        //из Request в объект
        CreateMap<WorkspaceTaskRequest, WorkspaceTask>()
           .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Status,
                src => src.MapFrom((x) => StatusTask.FromValue(x.Status)))
        //.ForMember(dest => dest.Notes,
        //   src => src.MapFrom(x => x.Notes));
        .ForPath(dest => dest.Manager.Id,
            src => src.MapFrom(x => x.ManagerId))
        .ForPath(dest => dest.Employee.Id,
            src => src.MapFrom(x => x.EmployeeId));

        //из объекта в DTO
        CreateMap<WorkspaceTask, WorkspaceTaskDTO>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Status,
                src => src.MapFrom(x => x.Status.IdStatus))
        //.ForMember(dest => dest.Notes,
        //   src => src.MapFrom(x => x.Notes));
        .ForPath(dest => dest.ManagerId,
            src => src.MapFrom(x => x.Manager.Id))
        .ForPath(dest => dest.EmployeeId,
            src => src.MapFrom(x => x.Employee.Id));

        //из WorkspaceTaskShortRequest в объект
        CreateMap<WorkspaceTaskShortRequest, WorkspaceTask>()
           .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Status,
                src => src.MapFrom((x) => StatusTask.FromValue(x.Status)));
        //.ForMember(dest => dest.Notes,
        //   src => src.MapFrom(x => x.Notes));
        //.ForMember(dest => dest.ManagerId, 
        //    src => src.MapFrom(x => x.ManagerId))
        //.ForMember(dest => dest.EmployeeId, 
        //    src => src.MapFrom(x => x.EmployeeId));

    }
}