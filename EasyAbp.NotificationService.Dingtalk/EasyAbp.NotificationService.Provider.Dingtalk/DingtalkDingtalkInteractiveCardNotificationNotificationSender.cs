using AlibabaCloud.SDK.Dingtalkim_1_0.Models;
using EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class DingtalkDingtalkInteractiveCardNotificationNotificationSender : IDingtalkInteractiveCardNotificationNotificationSender,ITransientDependency
{
    protected IDingtalkOAuth DingtalkOAuth { get; }
    protected IDingtalkRobot DingtalkRobot { get; }
    protected ILogger<DingtalkDingtalkInteractiveCardNotificationNotificationSender> Logger { get; }
    
    public DingtalkDingtalkInteractiveCardNotificationNotificationSender(ILogger<DingtalkDingtalkInteractiveCardNotificationNotificationSender> logger, 
        IDingtalkRobot dingtalkRobot, IDingtalkOAuth dingtalkOAuth)
    {
       
        Logger = logger;
        DingtalkRobot = dingtalkRobot;
        DingtalkOAuth = dingtalkOAuth;
    }

   
    public virtual async Task<SendRobotInteractiveCardResponse> SendAsync(string userId, DingtalkRobotInteractiveCardDataModel dataModel)
    {
        var accessTokenResponse = DingtalkOAuth.GetAccessToken();
        return DingtalkRobot.SendSingleChatInteractiveCards(accessTokenResponse.Body.AccessToken, dataModel.CardBizId, dataModel.CardData, userId, null);
    }
}