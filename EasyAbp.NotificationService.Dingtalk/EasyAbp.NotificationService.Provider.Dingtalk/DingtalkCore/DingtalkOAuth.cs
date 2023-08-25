using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Dingtalkoauth2_1_0;
using AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models;
using Microsoft.Extensions.Options;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public class DingtalkOAuth : IDingtalkOAuth
{
    private Configuration _configuration;
    private Client _client;

    public DingtalkOAuth(IOptions<Configuration> options)
    {
        _configuration = options.Value;
        _client = new Client(new Config()
        {
            Protocol = "https",
            RegionId = "central"
        });
    }

    /// <summary>
    /// 根据免登授权码获取用户 accesstoken,https://open.dingtalk.com/document/isvapp/obtain-user-token
    /// </summary>
    /// <param name="code">OAuth 2.0 临时授权码</param>
    /// <returns></returns>
    public GetUserTokenResponse GetUserToken(string code)
    {
        GetUserTokenRequest getUserTokenRequest = new GetUserTokenRequest
        {
            //应用id。可使用扫码登录应用或者第三方个人小程序的appId。
            //- 企业内部应用传应用的AppKey
            //- 第三方企业应用传应用的SuiteKey
            //- 第三方个人应用传应用的AppId
            ClientId = _configuration.AppKey,
            //应用密钥。
            //- 企业内部应用传应用的AppSecret
            //- 第三方企业应用传应用的SuiteSecret
            //- 第三方个人应用传应用的AppSecret
            ClientSecret = _configuration.AppSecret,
            //OAuth 2.0 临时授权码
            Code = code,
            //OAuth2.0刷新令牌，从返回结果里面获取。
            //RefreshToken = "abcd",
            //如果使用授权码换token，传authorization_code。
            //如果使用刷新token换用户token，传refresh_token。
            GrantType = "authorization_code",
        };
        return DingtalkUtil.ExecuteAndCatchException(() => _client.GetUserToken(getUserTokenRequest));
    }
    
    /// <summary>
    /// 获取第三方应用授权企业的accessToken,https://open.dingtalk.com/document/isvapp/obtain-the-access_token-of-the-authorized-enterprise
    /// </summary>
    /// <returns></returns>
    public GetAccessTokenResponse GetAccessToken()
    {
        GetAccessTokenRequest getUserTokenRequest = new GetAccessTokenRequest
        {
            //已创建的企业内部应用的AppKey。
            AppKey = _configuration.AppKey,
            //已创建的企业内部应用的AppSecret。
            AppSecret = _configuration.AppSecret,
        };
        return DingtalkUtil.ExecuteAndCatchException(() => _client.GetAccessToken(getUserTokenRequest));
    }
}