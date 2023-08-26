
namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IAbpDingtalkServiceFactory
{
    /// <summary>
    /// 使用本方法实例化一个钉钉服务
    /// </summary>
    /// <param name="appId">目标钉钉应用的 appid，如果为空则取 Setting 中的默认值</param>
    /// <typeparam name="TService">任意钉钉`服务类型</typeparam>
    /// <returns></returns>
    Task<TService> CreateAsync<TService>(string appId = null) where TService : IAbpDingtalkService;
}