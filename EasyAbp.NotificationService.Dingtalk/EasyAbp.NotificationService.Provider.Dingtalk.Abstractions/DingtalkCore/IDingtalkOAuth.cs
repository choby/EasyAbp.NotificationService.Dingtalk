using AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public interface IDingtalkOAuth
{
    /// <summary>
    /// 根据免登授权码获取用户 accesstoken,https://open.dingtalk.com/document/isvapp/obtain-user-token
    /// </summary>
    /// <param name="code">OAuth 2.0 临时授权码</param>
    /// <returns></returns>
    GetUserTokenResponse GetUserToken(string code);
    
    /// <summary>
    /// 获取第三方应用授权企业的accessToken,https://open.dingtalk.com/document/isvapp/obtain-the-access_token-of-the-authorized-enterprise
    /// </summary>
    /// <returns></returns>
    GetAccessTokenResponse GetAccessToken();
}