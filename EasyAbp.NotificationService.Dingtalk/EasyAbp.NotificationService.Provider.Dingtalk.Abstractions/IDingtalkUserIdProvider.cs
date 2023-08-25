
namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IDingtalkUserIdProvider
{
    Task<string> GetOrNullAsync(string appId, Guid userId);
    Task<string> GetOrNullAsync(Guid userId);
}