using AlibabaCloud.SDK.Dingtalkcard_1_0.Models;
using AlibabaCloud.SDK.Dingtalkim_1_0.Models;
using EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class InteractiveCardNotificationNotificationSender : IInteractiveCardNotificationNotificationSender,ITransientDependency
{
    protected IDingtalkOAuth DingtalkOAuth { get; }
    protected IDingtalkRobot DingtalkRobot { get; }
    protected IDingtalkCard DingtalkCard { get; }
    protected ILogger<InteractiveCardNotificationNotificationSender> Logger { get; }
    
    public InteractiveCardNotificationNotificationSender(ILogger<InteractiveCardNotificationNotificationSender> logger, 
        IDingtalkRobot dingtalkRobot, IDingtalkOAuth dingtalkOAuth, IDingtalkCard dingtalkCard)
    {
       
        Logger = logger;
        DingtalkRobot = dingtalkRobot;
        DingtalkOAuth = dingtalkOAuth;
        DingtalkCard = dingtalkCard;
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
    
    public virtual async Task<CreateAndDeliverResponse> CreateAndDeliverCardsAsync(InteractiveCardDataModel dataModel, string userId = null)
    {
        var accessTokenResponse = DingtalkOAuth.GetAccessToken();
        return DingtalkCard.CreateAndDeliverCards(accessTokenResponse.Body.AccessToken, dataModel, userId);
    }

    public virtual async Task<UpdateCardResponse> UpdateCardsAsync(UpdateCardRequest updateCardRequest,
        UpdateCardRequest.UpdateCardRequestCardUpdateOptions updateCardRequestCardUpdateOptions = null)
    {
        var accessTokenResponse = DingtalkOAuth.GetAccessToken();
        return DingtalkCard.UpdateCard(accessTokenResponse.Body.AccessToken, updateCardRequest, updateCardRequestCardUpdateOptions);
    }
}