using AutoMapper;

using Workspace.DAL;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class UserGhost
    {
        private WorkspaceUser _user;
        private WorkspaceTask _task;
        private IMapper _mapper;
        private ITaskRepository _taskRepository;        
        private IOrgRole _role;
        public UserGhost(IMapper mapper, ITaskRepository taskRepository, WorkspaceUser user, WorkspaceTask task)
        {
            _user = user;
            _task = task;
            _taskRepository = taskRepository;
            _mapper = mapper;
            SetOrgRole();
        }

        public void ChangeStatus(StatusTask statusTask)
        {
            _role.ChangeStatus(statusTask);
        }

        private async void SetOrgRole() 
        {
            var userDTO = _mapper.Map<WorkspaceUserDTO>(_user);
            var taskDTO = _mapper.Map<WorkspaceTaskDTO>(_task);

            
            if (_taskRepository.IsTaskOwner(taskDTO, userDTO))
            {
                _role = new Manager(_mapper, _taskRepository, _user, _task);
            }
            else 
            {
                _role = new Employee(_mapper, _taskRepository, _user, _task);
            }
        }
    }
}