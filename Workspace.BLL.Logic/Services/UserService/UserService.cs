using AutoMapper;

using Workspace.BLL.Logic.Contracts;
using Workspace.DAL;
using Workspace.Entities;
using Workspace.Auth;
using Microsoft.Extensions.Logging;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class UserService(IMapper mapper, IUserRepository userRepository,
    IWorkspacePasswordHasher passwordHasher, IJwtProvider jwtProvider, ILogger<UserService> logger) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IWorkspacePasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ILogger _logger = logger;

    public async Task<List<WorkspaceUserResponse>> GetAllAsync()
    {
        try
        {
            var workspaceUsers = new List<WorkspaceUserResponse>();
            var workspaceUserResponse = new List<WorkspaceUserResponse>();

            var workspaceUserResponsesDTO = await _userRepository.GetAllUsersAsync();

            workspaceUsers = _mapper.Map<List<WorkspaceUserResponse>>(workspaceUserResponsesDTO);

            workspaceUserResponse = _mapper.Map<List<WorkspaceUserResponse>>(workspaceUsers);

            return workspaceUsers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAllAsync");
            throw;
        }
    }

    public async Task<WorkspaceUserResponse?> GetByIdAsync(Guid id)
    {
        try
        {
            var workspaceUsers = new WorkspaceUser();
            var workspaceUsersResponse = new WorkspaceUserResponse();

            var workspaceUsersDTO = await _userRepository.GetByIDAsync(id);

            workspaceUsers = _mapper.Map<WorkspaceUser>(workspaceUsersDTO);
            workspaceUsersResponse = _mapper.Map<WorkspaceUserResponse>(workspaceUsers);

            return workspaceUsersResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetByIdAsync");
            throw;
        }
    }

    //Регистрация пользователя
    public async Task<string> LoginAsync(WorkspaceUserRequest workspaceUserRequest)
    {
        try
        {
            var workspaceUser = new WorkspaceUser();
            var workspaceUserDTO = await _userRepository.GetByUserLoginAsync(workspaceUserRequest.Login);
            if (workspaceUserDTO is null)
            {
                return "Нет записи";
            }

            workspaceUser = _mapper.Map<WorkspaceUser>(workspaceUserDTO);

            //проверка пароля
            var result = _passwordHasher.Verify(workspaceUserRequest.Password, workspaceUser.Password);
            if (result is false)
            {
                throw new Exception("Пароль введен не верно");
            }

            var token = _jwtProvider.GenerateToken(workspaceUser);

            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в LoginAsync");
            throw;
        }
    }

    public async Task UpdateAsync(Guid id, WorkspaceUserRequest workspaceUserRequest)
    {
        try
        {
            var _hashedPassword = _passwordHasher.Generate(workspaceUserRequest.Password);
            var _workspaceUser = new WorkspaceUser();
            var _workspaceUserDTO = new WorkspaceUserDTO();

            _workspaceUser = _mapper.Map<WorkspaceUser>(workspaceUserRequest);
            _workspaceUser.Id = id;
            _workspaceUser.Password = _hashedPassword;

            _workspaceUserDTO = _mapper.Map<WorkspaceUserDTO>(_workspaceUser);

            await _userRepository.UpdateAsync(_workspaceUserDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в UpdateAsync");
            throw;
        }
    }

    public async Task CreateAsync(WorkspaceUserRequest workspaceUserRequest)
    {
        try
        {
            var isExistLogin = await _userRepository.GetByUserLoginAsync(workspaceUserRequest.Login);

            if (isExistLogin?.Login is not null)
            {
                throw new Exception("Данный логин пользователя уже есть в БД");
            }

            var _hashedPassword = _passwordHasher.Generate(workspaceUserRequest.Password);
            var _workspaceUser = new WorkspaceUser();
            var _workspaceUserDTO = new WorkspaceUserDTO();

            _workspaceUser = _mapper.Map<WorkspaceUser>(workspaceUserRequest);
            _workspaceUser.Password = _hashedPassword;
            _workspaceUserDTO = _mapper.Map<WorkspaceUserDTO>(_workspaceUser);

            await _userRepository.CreateAsync(_workspaceUserDTO);
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
            await _userRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteAsync");
            throw;
        }
    }
    public async Task CreateUserRoleAsync (WorkspaceUserRoleRequest workspaceUserRoleRequest)
    {
        try
        {
            var _workspaceUserRoleDTO = _mapper.Map<WorkspaceUserRoleDTO>(workspaceUserRoleRequest);
            await _userRepository.CreateUserRoleAsync(_workspaceUserRoleDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в CreateUserRoleAsync");
            throw;
        }
    }
    public async Task DeleteUserRoleAsync (WorkspaceUserRoleRequest workspaceUserRoleRequest)
    {
        try
        {
            var _workspaceUserRoleDTO = _mapper.Map<WorkspaceUserRoleDTO>(workspaceUserRoleRequest);
            await _userRepository.DeleteUserRoleAsync(_workspaceUserRoleDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в DeleteUserRoleAsync");
            throw;
        }
    }

    public async Task<List<RoleResponse>> GetAllRolesAsync(Guid id)
    {
        try
        {
            var _rolesResponse = new List<RoleResponse>();
            var _rolesDTO = await _userRepository.GetUserRolesAsync(id);
            _rolesResponse = _mapper.Map<List<RoleResponse>>(_rolesDTO);

            return _rolesResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в GetAllRolesAsync");
            throw;
        }
    }

    public void ChangeStatus(IWorkspaceUser user, IWorkspaceTask task)
    {
        /*
         * если нельзя установить статус, то 
         */
        //user.Status = ChangeStatus(task);
        /*статус не менять*/
        //task.ChangeStatus();
    }
}

