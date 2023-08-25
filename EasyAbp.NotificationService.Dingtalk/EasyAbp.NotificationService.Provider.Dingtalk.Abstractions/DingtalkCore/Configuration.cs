namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public class Configuration
{
    public string AppKey { get; set; }
    public string AppSecret { get; set; }
    public string RobotCode { get; set; }
    public string RedirectUri { get; set; }
    public string WebHookAesKey { get; set; }
    public string WebHookToken { get; set; }
}