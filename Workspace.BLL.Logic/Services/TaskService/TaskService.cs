using AutoMapper;
using Microsoft.Extensions.Logging;

using Workspace.DAL;
using Workspace.BLL.Logic.Contracts;
using Workspace.Entities;
using Workspace.Entities.Contracts;
namespace Workspace.BLL.Logic;

public class TaskService (IMapper mapper , ITaskRepository taskRepository, ILogger<TaskService> logger) : ITaskService
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async IAsyncEnumerable<WorkspaceTaskResponse> GetAllAsync()
    {
        await foreach (var task in _taskRepository.GetAllTasksAsync())
        {
            yield return _mapper.Map<WorkspaceTaskResponse>(task);
        }
    }

    public async Task<WorkspaceTaskResponse?> GetByIdAsync(Guid id)
    {
        try
        {
            var workspaceTask = new WorkspaceTask();
            var workspaceTaskResponse = new WorkspaceTaskResponse();
             
            var workspaceTaskDto = await _taskRepository.GetByIDAsync(id);

            workspaceTask = _mapper.Map<WorkspaceTask>(workspaceTaskDto);
            workspaceTaskResponse = _mapper.Map<WorkspaceTaskResponse>(workspaceTask);

            return workspaceTaskResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetByIdAsync");
            throw;
        }
    }
    
    public async Task UpdateAsync(Guid id, WorkspaceTaskRequest workspaceTaskRequest)
    {
        try
        {
            //проверяем, а есть ли в БД такой объект
            var workspaceTaskDto = await _taskRepository.GetByIDAsync(id);
            if (workspaceTaskDto is null)
            {
                return;
            }
        
            //Создаем объект
            var workspaceTask = new WorkspaceTask();
            workspaceTask = _mapper.Map<WorkspaceTask>(workspaceTaskRequest);
            workspaceTask.Id = id;

            workspaceTaskDto = _mapper.Map<WorkspaceTaskDTO>(workspaceTask);

            workspaceTaskDto.Status = workspaceTaskDto.Status > workspaceTaskRequest.Status ? workspaceTaskDto.Status : workspaceTaskRequest.Status;

            //TODO: Необходимо доделать валидацию, так как только валидный Task сохранится в БД 
            //Валидация логики:
            // (1) . нельзя менять статус с нарушением последоавтельности, за 
            // исключением, когда вернули на доработку 
            // (2) нельзя менять статусы в обратном порядке 

            //Если валидация прошла успешно, обновляем БД 
            await _taskRepository.UpdateAsync(workspaceTaskDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в UpdateAsync");
            throw;
        }

    }

    public async Task CreateAsync(WorkspaceTaskRequest workspaceTaskRequest)
    {
        try
        {
            var _workspaceTask = new WorkspaceTask();
            var _workspaceTaskDTO = new WorkspaceTaskDTO();
            var _user = new WorkspaceUser();
            _workspaceTask.Manager = _user;

            _workspaceTask = _mapper.Map<WorkspaceTask>(workspaceTaskRequest);

            _workspaceTaskDTO = _mapper.Map<WorkspaceTaskDTO>(_workspaceTask);

            await _taskRepository.CreateAsync(_workspaceTaskDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateAsync");
            throw;
        }
    }
    
  public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _taskRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteAsync");
            throw;
        }
    }

    public async Task<List<WorkspaceTaskResponse>> GetAllTasksForMartAsync(Guid martId)
    {
        try
        {
            var workspaceTasks = new List<WorkspaceTask>();
            var workspaceTaskResponse = new List<WorkspaceTaskResponse>();

            var workspaceTaskResponsesDTO = await _taskRepository.GetAllTasksForMartAsync(martId);

            workspaceTasks = _mapper.Map<List<WorkspaceTask>>(workspaceTaskResponsesDTO);

            workspaceTaskResponse = _mapper.Map<List<WorkspaceTaskResponse>>(workspaceTasks);

            return workspaceTaskResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAllTasksForMartAsync");
            throw;
        }
    }

    public async Task SetEmployeeAsync(WorkspaceTaskEmployee workspaceTaskEmployee)
    {
        try
        {
            //проверяем, а есть ли в БД такой объект
            var workspaceTaskDto = await _taskRepository.GetByIDAsync(workspaceTaskEmployee.TaskID);

            var workspaceTask = new WorkspaceTask();
            workspaceTask = _mapper.Map<WorkspaceTask>(workspaceTaskEmployee);
                        
            workspaceTaskDto = _mapper.Map<WorkspaceTaskDTO>(workspaceTask);
            //workspaceTaskDto.EmployeeId = workspaceTaskRequest.EmployeeId;
            await _taskRepository.SetEmployeeAsync(workspaceTaskDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в UpdateAsync");
            throw;
        }

    }

    public async Task ChangeStatus(TaskUserRequest taskUserRequest)
    {
        WorkspaceUser _user = new();
        _user.Id = taskUserRequest.UserId;

        var _taskDTO = await _taskRepository.GetByIDAsync(taskUserRequest.TaskId);
        var _task = _mapper.Map<WorkspaceTask>(_taskDTO);
        var ghost = new UserGhost(_mapper, _taskRepository, _user, _task);
        ghost.ChangeStatus(_task.Status);
    }
}