using Workspace.BLL.Logic.Contracts;
using Workspace.Entities.Contracts;
using Workspace.Entities;
using Workspace.DAL;
using AutoMapper;

namespace Workspace.BLL.Logic;

public class WorkspaceMartService(IUserService userService, IMapper mapper, IMartRepository martRepository) : IWorkspaceMartService
{
    private readonly IUserService _userService = userService;
    private readonly IMartRepository _martRepository = martRepository;
    private readonly IMapper _mapper = mapper;

    //меням статус у таска
    //public void ChangeStatus(IWorkspaceUser workspaceUser, IWorkspaceTask workspaceTask)
    //{
    //    IWorkspaceRole _workspaceRole;

    //    //сравниваем owner и если да
    //    if (workspaceUser.Id == workspaceTask.Manager.Id)
    //    {
    //        _workspaceRole = new Manager(workspaceUser, workspaceTask);
    //    }
    //    else
    //    {
    //        _workspaceRole = new Employee(workspaceUser, workspaceTask);
    //    }

    //    _workspaceRole.ChangeStatus(workspaceTask);
    //}

    ///<summary>
    /// Создаем рабочее пространство
    /// </summary>
    /// <param name="workspaceMartRequest"></param>
    public async Task<WorkspaceMartResponse> CreateWorkpaceMartAsync(WorkspaceMartRequest workspaceMartRequest)
    {
        var workspaceMartDTO = _mapper.Map<WorkspaceMartDTO>(workspaceMartRequest);

        //сохраняем в БД
        var result = await _martRepository.CreateMartAsync(workspaceMartDTO);

        //возвращаем WorkspaceMartResponse
        var workspaceMartResponse = _mapper.Map<WorkspaceMartResponse>(result);
   
        return workspaceMartResponse;
    }

    //создаем таск
    public async Task<WorkspaceTaskResponse> CreateTaskAsync(Guid id, WorkspaceTaskShortRequest workspaceTaskRequest)
    {
        var _workspace = new WorkspaceMart()
        {
            Id = id
        };
        var _task = _mapper.Map<WorkspaceTask>(workspaceTaskRequest);
        var _taskDTO = _mapper.Map<WorkspaceTaskDTO>(_task);

        var _martDTO = new WorkspaceMartDTO()
        {
            Id = id
        };

        var _workspaceTaskDTO = await _martRepository.CreateTaskAsync(_martDTO, _taskDTO);
        var _workspaceTaskResponse = new WorkspaceTaskResponse()
        {
            Id = _workspaceTaskDTO.Id,
            Name = _taskDTO.Name,
            Status = 1,
            ManagerId = _workspaceTaskDTO.ManagerId,
            EmployeeId = Guid.Empty
        };
        return _workspaceTaskResponse;
    }
}