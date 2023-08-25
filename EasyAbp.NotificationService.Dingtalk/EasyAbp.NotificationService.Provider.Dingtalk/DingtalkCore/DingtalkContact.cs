using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Dingtalkcontact_1_0;
using AlibabaCloud.SDK.Dingtalkcontact_1_0.Models;
using AlibabaCloud.TeaUtil.Models;
using Microsoft.Extensions.Options;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public class DingtalkContact : IDingtalkContact
{
    private Configuration _dingtalkConfiguration;
    private Client _client;

    public DingtalkContact(IOptions<Configuration> options)
    {
        _dingtalkConfiguration = options.Value;
        _client = new Client(new Config()
        {
            Protocol = "https",
            RegionId = "central"
        });
    }

    /// <summary>
    /// 调用本接口获取企业用户通讯录中的个人信息。
    /// </summary>
    /// <param name="accessToken">调用服务端接口的授权凭证。使用个人用户的accessToken</param>
    /// <param name="unionId">用户的unionId。如需获取当前授权人的信息，unionId参数可以传me。</param>
    /// <returns></returns>
    public GetUserResponse GetUser(string accessToken, string unionId)
    {
        var getUserHeaders = new GetUserHeaders()
        {
            XAcsDingtalkAccessToken = accessToken
        };
        return DingtalkUtil.ExecuteAndCatchException(() => _client.GetUserWithOptions(unionId, getUserHeaders, new RuntimeOptions()));
    }
}