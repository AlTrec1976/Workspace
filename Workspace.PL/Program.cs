using AutoMapper;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Workspace.Auth;
using Workspace.BLL.Logic;
using Workspace.DAL;
using Workspace.PL;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApiAuthentification(configuration);
builder.Services.ConfigureAuth();

builder.Services.ConfigureDependencies();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new GuidModelBinderProvider());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "MaximaToDo";
    config.Title = "MaximaToDo v1";
    config.Version = "v1";
});

builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "Workspace.PL.xml");
    options.IncludeXmlComments(xmlPath);
}
);


var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new WorkspaceTaskProfile());
    mc.AddProfile(new WorkspaceUserProfile());
    mc.AddProfile(new WorkspaceNoteProfile());
    mc.AddProfile(new InviteProfile());
    mc.AddProfile(new WorkspaceMartProfile());
});

IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "MaximaToDo";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();