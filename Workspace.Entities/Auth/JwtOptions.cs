namespace Workspace.Auth;

public class JwtOptions :IJwtOptions
{
    //секретный ключ
    public string SecretKey { get; set; } = string.Empty;
    //сколько часов будет действовать токен
    public int ExpitesHours { get; set; } = default;
}