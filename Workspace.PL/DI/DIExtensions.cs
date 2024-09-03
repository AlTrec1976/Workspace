using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Workspace.Auth;
using Workspace.BLL.Logic;
using Workspace.BLL.Logic.Contracts;
using Workspace.BLL.Logic.Services.PermissionService;
using Workspace.DAL;
using Workspace.Entities.Contracts;
using Workspace.Entities;

namespace Workspace.PL;

public static class DIExtensions
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<INoteRepository, NoteRepository>();

        //
        services.AddScoped<IWorkspaceMartService, WorkspaceMartService>();
        services.AddScoped<IMartRepository,MartRepository>();
        services.AddScoped<IInviteRepository, InviteRepository>();
        services.AddScoped<IInviteService, InviteService>();
        services.AddScoped<ISendboxRepository,SendboxRepository>();
        services.AddScoped<ISendboxService, SendboxService>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IAdminService, AdminService>();
        return services;
    }

    public static IServiceCollection ConfigureAuth(this IServiceCollection services)
    {
        services.AddScoped<IWorkspacePasswordHasher, WorkspacePasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        
        return services;
    }

    public static void AddApiAuthentification(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                
                options.TokenValidationParameters = new()
                    {
                           ValidateIssuer = false,
                           ValidateAudience = false,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(
                               Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };
                
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Request.Cookies.TryGetValue("maxima-sec-cookies",out var accessToken);
                        context.Token = accessToken;
                       
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddAuthorization();
    }
}