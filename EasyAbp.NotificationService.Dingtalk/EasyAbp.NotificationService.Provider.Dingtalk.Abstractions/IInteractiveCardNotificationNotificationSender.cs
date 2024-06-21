using AlibabaCloud.SDK.Dingtalkcard_1_0.Models;
using AlibabaCloud.SDK.Dingtalkim_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IInteractiveCardNotificationNotificationSender
{
    Task<SendInteractiveCardResponse> SendAsync(InteractiveCardDataModel dataModel);
    Task<UpdateInteractiveCardResponse> UpdateAsync(UpdateInteractiveCardRequest updateInteractiveCardRequest,
        UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardOptions cardOptions = null);

    Task< CreateAndDeliverResponse> CreateAndDeliverCardsAsync(InteractiveCardDataModel dataModel, string userId = null);

    Task<UpdateCardResponse> UpdateCardsAsync(UpdateCardRequest updateCardRequest,
        UpdateCardRequest.UpdateCardRequestCardUpdateOptions updateCardRequestCardUpdateOptions = null);
}