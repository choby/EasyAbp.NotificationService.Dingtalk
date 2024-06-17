using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Dingtalkim_1_0;
using AlibabaCloud.SDK.Dingtalkim_1_0.Models;
using AlibabaCloud.TeaUtil.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public class DingtalkRobot : IDingtalkRobot
{
    private Configuration _configuration;
    private Client _client;
    private IJsonSerializer _jsonSerializer;

    public DingtalkRobot(IOptions<Configuration> options, IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
        _configuration = options.Value;
        _client = new Client(new Config()
        {
            Protocol = "https",
            RegionId = "central",
            DisableHttp2 = true
        });
    }

    /// <summary>
    /// 单聊机器人发送互动卡片（普通版）,https://open.dingtalk.com/document/orgapp/robots-send-interactive-cards
    /// </summary>
    /// <summary>
    /// 调试地址：https://open-dev.dingtalk.com/apiExplorer?spm=ding_open_doc.document.0.0.225e7369tLbSB3#/?devType=org&api=im_1.0%23SendRobotInteractiveCard
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="cardBizId">唯一标识一张卡片的外部ID，卡片幂等ID，可用于更新或重复发送同一卡片到多个群会话。</param>
    /// <param name="cardData">卡片模板文本内容参数，卡片json结构体。https://card.dingtalk.com/card-builder?spm=ding_open_doc.document.0.0.7a3f4a97AZtyRK</param>
    /// <param name="userId">userId和unionId二选一</param>
    /// <param name="unionId">userId和unionId二选一</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public SendRobotInteractiveCardResponse SendSingleChatInteractiveCards(string accessToken, string cardBizId, object cardData, [CanBeNull] string userId, [CanBeNull] string unionId)
    {
        if (string.IsNullOrWhiteSpace(userId) && string.IsNullOrWhiteSpace(unionId))
            throw new Exception("接收用户userId或者unionId不能为空");
        SendRobotInteractiveCardHeaders sendRobotInteractiveCardHeaders = new SendRobotInteractiveCardHeaders();
        sendRobotInteractiveCardHeaders.XAcsDingtalkAccessToken = accessToken;
        SendRobotInteractiveCardRequest sendRobotInteractiveCardRequest = new SendRobotInteractiveCardRequest()
        {
            SingleChatReceiver = !string.IsNullOrWhiteSpace(userId) ? _jsonSerializer.Serialize(new { userId }) : _jsonSerializer.Serialize(new { unionId }),
            CardTemplateId = "StandardCard",
            CardData = _jsonSerializer.Serialize(cardData),
            CardBizId = cardBizId,
            RobotCode = _configuration.RobotCode
        };

        return DingtalkUtil.ExecuteAndCatchException(() => _client.SendRobotInteractiveCardWithOptions(sendRobotInteractiveCardRequest, sendRobotInteractiveCardHeaders, new RuntimeOptions()));
    }
    
    /// <summary>
    /// 单聊机器人发送互动卡片（普通版）,https://open.dingtalk.com/document/orgapp/robots-send-interactive-cards
    /// </summary>
    /// <summary>
    /// 调试地址：https://open-dev.dingtalk.com/apiExplorer?spm=ding_open_doc.document.0.0.225e7369tLbSB3#/?devType=org&api=im_1.0%23SendRobotInteractiveCard
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="dataModel"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public SendRobotInteractiveCardResponse SendRobotInteractiveCards(string accessToken, RobotInteractiveCardDataModel dataModel)
    {
        if (string.IsNullOrWhiteSpace(dataModel.SingleChatReceiver))
            throw new Exception("接收用户不能为空");
        SendRobotInteractiveCardHeaders sendRobotInteractiveCardHeaders = new SendRobotInteractiveCardHeaders();
        sendRobotInteractiveCardHeaders.XAcsDingtalkAccessToken = accessToken;
        SendRobotInteractiveCardRequest sendRobotInteractiveCardRequest = new SendRobotInteractiveCardRequest()
        {
            CardTemplateId =dataModel.CardTemplateId,
            OpenConversationId = dataModel.OpenConversationId,
            SingleChatReceiver = dataModel.SingleChatReceiver,
            CardBizId = dataModel.CardBizId,
            RobotCode =_configuration.RobotCode,
            CallbackUrl = dataModel.CallbackUrl,
            CardData = dataModel.CardData,
            UserIdPrivateDataMap = dataModel.UserIdPrivateDataMap,
            UnionIdPrivateDataMap =dataModel.UnionIdPrivateDataMap,
            SendOptions = dataModel.SendOptions,
            PullStrategy = dataModel.PullStrategy,
        };

        return DingtalkUtil.ExecuteAndCatchException(() => _client.SendRobotInteractiveCardWithOptions(sendRobotInteractiveCardRequest, sendRobotInteractiveCardHeaders, new RuntimeOptions()));
    }
    
    /// <summary>
    /// 发送互动卡片（高级版）,https://open.dingtalk.com/document/orgapp/send-interactive-dynamic-cards-1
    /// </summary>
    /// <summary>
    /// 调试地址：https://open-dev.dingtalk.com/apiExplorer?spm=ding_open_doc.document.0.0.225e7369tLbSB3#/?devType=org&api=robot_1.0%23OrgGroupSend
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="dataModel"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public SendInteractiveCardResponse SendInteractiveCards(string accessToken, InteractiveCardDataModel dataModel)
    {
        SendInteractiveCardHeaders sendInteractiveCardHeaders = new SendInteractiveCardHeaders();
        sendInteractiveCardHeaders.XAcsDingtalkAccessToken = accessToken;
        SendInteractiveCardRequest sendRobotInteractiveCardRequest = new SendInteractiveCardRequest()
        {
            CardTemplateId = dataModel.CardTemplateId,
            OpenConversationId = dataModel.OpenConversationId,
            ReceiverUserIdList = dataModel.ReceiverUserIdList,
            OutTrackId = dataModel.OutTrackId,
            RobotCode = _configuration.RobotCode,
            ConversationType = (int?)dataModel.ConversationType,
            CallbackRouteKey = dataModel.CallbackRouteKey,
            CardData = dataModel.CardData,
            PrivateData = dataModel.PrivateData,
            ChatBotId = dataModel.ChatBotId,
            UserIdType = (int?)dataModel.UserIdType,
            AtOpenIds = dataModel.AtOpenIds,
            CardOptions = dataModel.CardOptions,
            PullStrategy = dataModel.PullStrategy,
        };

        return DingtalkUtil.ExecuteAndCatchException(() => _client.SendInteractiveCardWithOptions(sendRobotInteractiveCardRequest, sendInteractiveCardHeaders, new RuntimeOptions()));
    }
    
    public  UpdateInteractiveCardResponse UpdateInteractiveCard(string accessToken, 
        UpdateInteractiveCardRequest updateInteractiveCardRequest, 
        UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardOptions cardOptions = null)
    {
        UpdateInteractiveCardHeaders updateInteractiveCardHeaders = new UpdateInteractiveCardHeaders();
        updateInteractiveCardHeaders.XAcsDingtalkAccessToken = accessToken;
        updateInteractiveCardRequest.CardOptions = cardOptions ?? new UpdateInteractiveCardRequest.UpdateInteractiveCardRequestCardOptions()
        {
            UpdateCardDataByKey = true,
            UpdatePrivateDataByKey = true,
        };
        return DingtalkUtil.ExecuteAndCatchException(() => _client.UpdateInteractiveCardWithOptions(updateInteractiveCardRequest, updateInteractiveCardHeaders, new RuntimeOptions()));
    }
}