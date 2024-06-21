using AlibabaCloud.SDK.Dingtalkcard_1_0.Models;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public interface IDingtalkCard
{
    /// <summary>
    /// 投放互动卡片,https://open.dingtalk.com/document/orgapp/delivery-card-interface
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="dataModel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    DeliverCardResponse DeliverCards(string accessToken, InteractiveCardDataModel dataModel, string userId = null);

    /// <summary>
    /// https://open.dingtalk.com/document/orgapp/create-and-deliver-cards
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="dataModel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    CreateAndDeliverResponse CreateAndDeliverCards(string accessToken, InteractiveCardDataModel dataModel, string userId = null);
    /// <summary>
    /// 更新卡片,https://open.dingtalk.com/document/orgapp/interactive-card-update-interface
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="updateCardRequest"></param>
    /// <param name="updateCardRequestCardUpdateOptions"></param>
    /// <returns></returns>
    public UpdateCardResponse UpdateCard(string accessToken,
        UpdateCardRequest updateCardRequest,
        UpdateCardRequest.UpdateCardRequestCardUpdateOptions updateCardRequestCardUpdateOptions = null
    );
}