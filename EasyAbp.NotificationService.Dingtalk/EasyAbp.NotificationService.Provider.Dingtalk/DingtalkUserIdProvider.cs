using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

[Dependency(TryRegister = true)]
public class DingtalkUserIdProvider : IDingtalkUserIdProvider,ITransientDependency
{
    public Task<string> GetOrNullAsync(string appId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetOrNullAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}