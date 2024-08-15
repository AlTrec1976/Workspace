using Workspace.BLL.Logic.Contracts;
using Workspace.Entities.Contracts;
using Workspace.Entities;
using Workspace.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Workspace.BLL.Logic;

public class WorkspaceMartService : IWorkspaceMartService
{
    private readonly InviteService _inviteService;
    private readonly IUserService _userService;
    private readonly IMartRepository _martRepository;
    private readonly IMapper _mapper;

    //public WorkspaceMartService(InviteService inviteService, IUserRepository userRepository, IMapper mapper, IMartRepository martRepository)
    public WorkspaceMartService( IUserService userService, IMapper mapper, IMartRepository martRepository)
    {
        _userService = userService;
        _mapper = mapper;
        _martRepository = martRepository;
    }

    //меням статус у таска
    public void ChangeStatus(IWorkspaceUser workspaceUser, IWorkspaceTask workspaceTask)
    {
        IWorkspaceRole _workspaceRole;

        //сравниваем owner и если да
        if (workspaceUser.Id == workspaceTask.Manager.Id)
        {
            _workspaceRole = new Manager(workspaceUser, workspaceTask);
        }
        else
        {
            _workspaceRole = new Employee(workspaceUser, workspaceTask);
        }

        _workspaceRole.ChangeStatus(workspaceTask);
    }

    ///<summary>
    /// Создаем рабочее пространство
    /// </summary>
    /// <param name="workspaceMartRequest"></param>
    public async Task<WorkspaceMartResponse> CreateWorkpaceMartAsync(WorkspaceMartRequest workspaceMartRequest)
    {
        ///??????не пойму для чего тебе нужно вытаскивать WorkspaceMart, если дальше по коду его не юзаешь??????
        ///
        //получаем пользователя из БД по ИД
        //var _userResponse = await _userService.GetByIdAsync(workspaceMartRequest.OwnerId);
        //var _workspaceUser = _mapper.Map<WorkspaceUser>(_userResponse);

        ///todo: переделать с маппингом
        //var workspace = await _martRepository.GetMartAsync(workspaceMartRequest.OwnerId);

        //var workspace = new WorkspaceMart();
        //workspace.Name = workspaceMartRequest.Name;
        //workspace.Owner = _workspaceUser;



        var workspaceMartDTO = _mapper.Map<WorkspaceMartDTO>(workspaceMartRequest);

        ////делаем дто
        //var workspaceMartDTO = new WorkspaceMartDTO();

        //workspaceMartDTO.Name = workspaceMartRequest.Name;
        //workspaceMartDTO.OwnerId = workspaceMartRequest.OwnerId;

        //сохраняем в БД
        var result = await _martRepository.CreateMartAsync(workspaceMartDTO);
                
        //возвращаем WorkspaceMartResponse
        var workspaceMartResponse = _mapper.Map<WorkspaceMartResponse>(result);
        //return new WorkspaceMartResponse()
        //{
        //    OwnerId = result.OwnerId,
        //    Name = result.Name,
        //    Id = result.Id
        //};

        return workspaceMartResponse;
    }

    //создаем таск
    public async Task<WorkspaceTaskResponse> CreateTaskAsync(Guid id, WorkspaceTaskShortRequest workspaceTaskRequest)
    {
        var _workspace = new WorkspaceMart()
        {
            Id =id
        };
        var _task = _mapper.Map<WorkspaceTask>(workspaceTaskRequest);
        var _taskDTO = _mapper.Map<WorkspaceTaskDTO>(_task);

        //var _task = new WorkspaceTask()
        //{
        //    Name = workspaceTaskRequest.Name,
        //    Status = StatusTask.New
        //};

        //var _taskDTO = new WorkspaceTaskDTO()
        //{
        //    Name = _task.Name,
        //    Status = _task.Status.IdStatus,
        //};

        var _martDTO = new WorkspaceMartDTO() 
        {
            Id = id
        };

        var _workspaceTaskDTO = await _martRepository.CreateTaskAsync(_martDTO, _taskDTO);
        var _workspaceTaskResponse  = new WorkspaceTaskResponse() 
        {
            Id = _workspaceTaskDTO.Id,
            Name = _taskDTO.Name,
            Status = 1,
            ManagerId = _workspaceTaskDTO.ManagerId,
            EmployeeId = Guid.Empty
        };
        return _workspaceTaskResponse;
    }

    //приглашаем юзеров
    public void CreateInvite()
    {
        ////непосредственно воркспейс
        //var workspace = new WorkspaceMart();
        ////пользователь, который создал воркспейс
        //var userOwner = new WorkspaceUser();

        ////пригл
        //var invite = new Invite();
        //invite.WorkspaceMart = workspace;
        //workspace.Owner = userOwner;

        ////нужена таблица с инвайтами, чтобы пригласить юзеров
        //_inviteService.AddInvite(invite);

    }

    //добавляем приглашенного в ворк
    public void AddUserToWorkspace()
    {
        //конкретный ворк
        var workspace = new WorkspaceMart();
        //хозяин ворка
        var userOwner = new WorkspaceUser();

        workspace.Owner = userOwner;

        //приглашенный юзер
        var user = new WorkspaceUser();
        //лист приглашенных юзеров
        var users = new List<IWorkspaceUser>();
        //добавляем юзера
        users.Add(user);
        //присваиваем юзеров
        //workspace.Users = users;
    }



    //получаем рабочее пространство где owner
    public void GetWorkspaceForOwners()
    {


    }
    //получаем рабочее пространства где пользователи
    public void GetWorkspaceForUsers()
    {

    }
}
/*Новый пользователь
 *
 *
 *
 *
 * 
 */

/*
 * Принял приглашение
 */


/*
 *  Workspace 01
 * Workspace 02 ты создавал, значит ты менеджер
 */

/*
 * Захожу и только в этом момент система определяет меня как сотрудника
 */
