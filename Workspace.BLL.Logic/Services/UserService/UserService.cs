﻿using AutoMapper;

using Workspace.BLL.Logic.Contracts;
using Workspace.DAL;
using Workspace.Entities;
using Workspace.Auth;
using Microsoft.Extensions.Logging;
using Workspace.Entities.Contracts;

namespace Workspace.BLL.Logic;

public class UserService(AppDbContext context, IMapper mapper, IUserRepository userRepository, 
    IWorkspacePasswordHasher passwordHasher, IJwtProvider jwtProvider, ILogger<UserService> logger) : IUserService
{
    private readonly AppDbContext _context = context;
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

    //Аутентификация пользователя
   public async Task<string> LoginAsync(WorkspaceUserRequest workspaceUserRequest)
    {
        try
        {
            var workspaceUser = new WorkspaceUser();
            var workspaceUserDTO = await _userRepository.GetByUserLoginAsync(workspaceUserRequest.Login);
            if (workspaceUserDTO is null)
            {
                _logger.LogError("Нет такого пользователя");
                throw new ArgumentNullException("Нет такого пользователя");
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

            if (isExistLogin.Login is not null) 
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
