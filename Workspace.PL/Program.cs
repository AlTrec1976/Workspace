using AutoMapper;
using Microsoft.OpenApi.Models;

using Workspace.Auth;
using Workspace.BLL.Logic;
using Workspace.PL;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddApiAuthentification(configuration);
builder.Services.ConfigureAuth();
builder.Services.ConfigureDependencies();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "Workspace.PL.xml");
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Workspace API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "¬ведите валидный токен",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new WorkspaceTaskProfile());
    mc.AddProfile(new WorkspaceUserProfile());
    mc.AddProfile(new WorkspaceNoteProfile());
    mc.AddProfile(new InviteProfile());
    mc.AddProfile(new WorkspaceMartProfile());
    mc.AddProfile(new SendboxProfile());
    mc.AddProfile(new AdminProfile());
});

IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();