using AlibabaCloud.SDK.Dingtalkcontact_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public interface IDingtalkContact
{
    /// <summary>
    /// 调用本接口获取企业用户通讯录中的个人信息。
    /// </summary>
    /// <param name="accessToken">调用服务端接口的授权凭证。使用个人用户的accessToken</param>
    /// <param name="unionId">用户的unionId。如需获取当前授权人的信息，unionId参数可以传me。</param>
    /// <returns></returns>
    GetUserResponse GetUser(string accessToken, string unionId);
}