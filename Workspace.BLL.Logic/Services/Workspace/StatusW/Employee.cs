using AutoMapper;
using Workspace.DAL;
using Workspace.Entities;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class Employee(IMapper mapper, ITaskRepository taskRepository, IWorkspaceUser workspaceUser, IWorkspaceTask workspaceTask) : IOrgRole
{
    private readonly IWorkspaceUser _workspaceUser = workspaceUser;
    private readonly IWorkspaceTask _workspaceTask = workspaceTask;
    private readonly IMapper _mapper = mapper;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public IWorkspaceUser User { get { return _workspaceUser; } }

    public IWorkspaceTask Task { get { return _workspaceTask; } }

    public void ChangeStatus(StatusTask statusTask)
    {
     
        if (workspaceTask.Status == StatusTask.Assigned)
        {
            workspaceTask.ChangeStatus(StatusTask.Accepted);
        }
        else if (workspaceTask.Status == StatusTask.Accepted)
        {
            workspaceTask.ChangeStatus(StatusTask.JobInProgress);
        }
        else if (workspaceTask.Status == StatusTask.JobInProgress)
        {
            workspaceTask.ChangeStatus(StatusTask.JobDown);
        }
        Save();
    }

    private void Save()
    {
        var taskDTO = _mapper.Map<WorkspaceTaskDTO>(_workspaceTask);
        _taskRepository.UpdateStatus(taskDTO);
    }
}
