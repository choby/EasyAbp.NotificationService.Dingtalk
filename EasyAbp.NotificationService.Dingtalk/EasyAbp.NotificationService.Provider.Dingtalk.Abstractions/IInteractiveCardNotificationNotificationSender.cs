using AlibabaCloud.SDK.Dingtalkim_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IInteractiveCardNotificationNotificationSender
{
    Task<SendInteractiveCardResponse> SendAsync(InteractiveCardDataModel dataModel);
    Task<UpdateInteractiveCardResponse> UpdateAsync(UpdateInteractiveCardRequest updateInteractiveCardRequest,
        UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardOptions cardOptions = null);
}