using AlibabaCloud.SDK.Dingtalkim_1_0.Models;
using EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class InteractiveCardNotificationNotificationSender : IInteractiveCardNotificationNotificationSender,ITransientDependency
{
    protected IDingtalkOAuth DingtalkOAuth { get; }
    protected IDingtalkRobot DingtalkRobot { get; }
    protected ILogger<InteractiveCardNotificationNotificationSender> Logger { get; }
    
    public InteractiveCardNotificationNotificationSender(ILogger<InteractiveCardNotificationNotificationSender> logger, 
        IDingtalkRobot dingtalkRobot, IDingtalkOAuth dingtalkOAuth)
    {
       
        Logger = logger;
        DingtalkRobot = dingtalkRobot;
        DingtalkOAuth = dingtalkOAuth;
    }

   
    public virtual async Task<SendInteractiveCardResponse> SendAsync(InteractiveCardDataModel dataModel)
    {
        var accessTokenResponse = DingtalkOAuth.GetAccessToken();
        return DingtalkRobot.SendInteractiveCards(accessTokenResponse.Body.AccessToken, dataModel);
    }

    public virtual async Task<UpdateInteractiveCardResponse> UpdateAsync(UpdateInteractiveCardRequest updateInteractiveCardRequest,
        UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardOptions cardOptions = null)
    {
        var accessTokenResponse = DingtalkOAuth.GetAccessToken();
        return DingtalkRobot.UpdateInteractiveCard(accessTokenResponse.Body.AccessToken, updateInteractiveCardRequest, cardOptions);
    }
}