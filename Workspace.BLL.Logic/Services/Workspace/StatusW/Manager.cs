using AutoMapper;
using Workspace.DAL;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class Manager (IMapper mapper, ITaskRepository taskRepository, IWorkspaceUser workspaceUser, IWorkspaceTask workspaceTask) : IOrgRole
{
    private readonly IWorkspaceUser _workspaceUser = workspaceUser;
    private readonly IWorkspaceTask _workspaceTask = workspaceTask;
    private readonly IMapper _mapper = mapper;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public IWorkspaceUser User { get { return _workspaceUser; } }

    public IWorkspaceTask Task { get { return _workspaceTask; } }

    public async void ChangeStatus(StatusTask statusTask)
    {
        if (workspaceTask.Status == StatusTask.New)
        {
            workspaceTask.ChangeStatus(StatusTask.Assigned);
        }

        if (workspaceTask.Status == StatusTask.JobDown)
        {
            workspaceTask.Status = StatusTask.Completed;
        }
        var taskDTO = _mapper.Map<WorkspaceTaskDTO>(_workspaceTask);
        await _taskRepository.UpdateStatus(taskDTO);
    }
}
