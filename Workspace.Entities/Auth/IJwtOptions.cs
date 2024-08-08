namespace Workspace.Auth;

public interface IJwtOptions
{
    string SecretKey { get; set; }
    
    int ExpitesHours { get; set; }
}