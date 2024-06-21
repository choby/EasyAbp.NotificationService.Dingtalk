using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Dingtalkcard_1_0;
using AlibabaCloud.SDK.Dingtalkcard_1_0.Models;
using AlibabaCloud.SDK.Dingtalkim_1_0.Models;
using AlibabaCloud.TeaUtil.Models;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public class DingtalkCard : IDingtalkCard
{
    private Configuration _configuration;
    private Client _client;
    private IJsonSerializer _jsonSerializer;

    public DingtalkCard(IOptions<Configuration> options, IJsonSerializer jsonSerializer)
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
    /// 投放互动卡片,https://open.dingtalk.com/document/orgapp/delivery-card-interface
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="dataModel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public  DeliverCardResponse DeliverCards(string accessToken, InteractiveCardDataModel dataModel, string userId = null)
    {
        DeliverCardHeaders deliverCardHeaders = new DeliverCardHeaders();
        deliverCardHeaders.XAcsDingtalkAccessToken = accessToken;
        string spaceType = dataModel.ConversationType switch
        {
            ConversationType.Group => "IM_GROUP",
            ConversationType.Single => "IM_SINGLE",
            ConversationType.Robot => "IM_ROBOT",
            ConversationType.Top => "ONE_BOX",
            //ConversationType.CoFeed => "cooperation_feed",
            ConversationType.Doc => "DOC",
            _ => throw new Exception("不支持的会话类型")
        };
        string spaceId = dataModel.ConversationType switch
        {
            ConversationType.Group => dataModel.OpenConversationId,
            ConversationType.Single => userId,
            ConversationType.Robot => userId,
            ConversationType.Top => dataModel.OpenConversationId,
            //ConversationType.CoFeed => userId,
            ConversationType.Doc => dataModel.DocKey,
            _ => throw new Exception("不支持的会话类型")
        };
        DeliverCardRequest deliverCardRequest = new DeliverCardRequest()
        {
            OutTrackId = dataModel.OutTrackId,
            OpenSpaceId = $"dtv1.card//{spaceType}{spaceId}",
            
            ImSingleOpenDeliverModel = dataModel.ConversationType == ConversationType.Single ? new DeliverCardRequest.DeliverCardRequestImSingleOpenDeliverModel()
            {
                AtUserIds = dataModel.AtOpenIds
            } : null,
            ImRobotOpenDeliverModel = dataModel.ConversationType == ConversationType.Robot ? new DeliverCardRequest. DeliverCardRequestImRobotOpenDeliverModel()
            {
                SpaceType = "IM_ROBOT",
                RobotCode = _configuration.RobotCode,
            } : null,
            ImGroupOpenDeliverModel = dataModel.ConversationType == ConversationType.Group ? new DeliverCardRequest.DeliverCardRequestImGroupOpenDeliverModel()
            {
                RobotCode = _configuration.RobotCode,
                AtUserIds = dataModel.AtOpenIds,
                Recipients = dataModel.ReceiverUserIdList
            } : null,
            TopOpenDeliverModel = dataModel.ConversationType == ConversationType.Top ? new DeliverCardRequest.DeliverCardRequestTopOpenDeliverModel()
            {
                ExpiredTimeMillis = dataModel.TopExpiredTimeMillis,
                UserIds = dataModel.ReceiverUserIdList,
            } : null,
            UserIdType = (int?)dataModel.UserIdType,
            
        };
       

        return DingtalkUtil.ExecuteAndCatchException(() => _client.DeliverCardWithOptions(deliverCardRequest, deliverCardHeaders, new RuntimeOptions()));
    }
    
    /// <summary>
    /// 创建并投放卡片,https://open.dingtalk.com/document/orgapp/create-and-deliver-cards
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="dataModel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public  CreateAndDeliverResponse CreateAndDeliverCards(string accessToken, InteractiveCardDataModel dataModel, string userId = null)
    {
        CreateAndDeliverHeaders createAndDeliverHeaders = new CreateAndDeliverHeaders();
        createAndDeliverHeaders.XAcsDingtalkAccessToken = accessToken;
        string spaceType = dataModel.ConversationType switch
        {
            ConversationType.Group => "IM_GROUP",
            ConversationType.Single => "IM_SINGLE",
            ConversationType.Robot => "IM_ROBOT",
            ConversationType.Top => "ONE_BOX",
            //ConversationType.CoFeed => "cooperation_feed",
            ConversationType.Doc => "DOC",
            _ => throw new Exception("不支持的会话类型")
        };
        string spaceId = dataModel.ConversationType switch
        {
            ConversationType.Group => dataModel.OpenConversationId,
            ConversationType.Single => userId,
            ConversationType.Robot => userId,
            ConversationType.Top => dataModel.OpenConversationId,
            //ConversationType.CoFeed => userId,
            ConversationType.Doc => dataModel.DocKey,
            _ => throw new Exception("不支持的会话类型")
        };
        CreateAndDeliverRequest deliverCardRequest = new CreateAndDeliverRequest()
        {
            CardTemplateId = dataModel.CardTemplateId,
            CardData = dataModel.CardData,
            PrivateData = dataModel.PrivateData,
            OutTrackId = dataModel.OutTrackId,
            OpenSpaceId = $"dtv1.card//{spaceType}.{spaceId};",
            ImSingleOpenDeliverModel = dataModel.ConversationType == ConversationType.Single
                ? new CreateAndDeliverRequest.CreateAndDeliverRequestImSingleOpenDeliverModel()
                {
                    AtUserIds = dataModel.AtOpenIds
                }
                : null,
            ImSingleOpenSpaceModel = new CreateAndDeliverRequest.CreateAndDeliverRequestImSingleOpenSpaceModel()
            {
                Notification = new CreateAndDeliverRequest.CreateAndDeliverRequestImSingleOpenSpaceModel.CreateAndDeliverRequestImSingleOpenSpaceModelNotification()
                {
                    NotificationOff = false
                }
            },
            ImRobotOpenDeliverModel = dataModel.ConversationType == ConversationType.Robot
                ? new CreateAndDeliverRequest.CreateAndDeliverRequestImRobotOpenDeliverModel()
                {
                    SpaceType = "IM_ROBOT",
                    RobotCode = _configuration.RobotCode,
                }
                : null,
            ImRobotOpenSpaceModel = new CreateAndDeliverRequest.CreateAndDeliverRequestImRobotOpenSpaceModel()
            {
                Notification = new CreateAndDeliverRequest.CreateAndDeliverRequestImRobotOpenSpaceModel.CreateAndDeliverRequestImRobotOpenSpaceModelNotification()
                {
                    NotificationOff = false
                }
            },
            ImGroupOpenDeliverModel = dataModel.ConversationType == ConversationType.Group
                ? new CreateAndDeliverRequest.CreateAndDeliverRequestImGroupOpenDeliverModel()
                {
                    RobotCode = _configuration.RobotCode,
                    AtUserIds = dataModel.AtOpenIds,
                    
                }
                : null,
            ImGroupOpenSpaceModel = new CreateAndDeliverRequest.CreateAndDeliverRequestImGroupOpenSpaceModel()
            {
                Notification = new CreateAndDeliverRequest.CreateAndDeliverRequestImGroupOpenSpaceModel.CreateAndDeliverRequestImGroupOpenSpaceModelNotification()
                {
                    NotificationOff = false
                }
            },
           
            TopOpenDeliverModel = dataModel.ConversationType == ConversationType.Top
                ? new CreateAndDeliverRequest.CreateAndDeliverRequestTopOpenDeliverModel()
                {
                    ExpiredTimeMillis = dataModel.TopExpiredTimeMillis,
                    UserIds = dataModel.ReceiverUserIdList,
                }
                : null,
            TopOpenSpaceModel = new CreateAndDeliverRequest.CreateAndDeliverRequestTopOpenSpaceModel()
            {
                SpaceType = "ONE_BOX"
            },
            UserIdType = (int?)dataModel.UserIdType,
            CallbackType = dataModel.CallbackType == CallbackType.HTTP ? "HTTP" : "STREAM"
        };
       

        return DingtalkUtil.ExecuteAndCatchException(() => _client.CreateAndDeliverWithOptions(deliverCardRequest, createAndDeliverHeaders, new RuntimeOptions()));
    }
    
    /// <summary>
    /// 更新卡片,https://open.dingtalk.com/document/orgapp/interactive-card-update-interface
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="updateCardRequest"></param>
    /// <param name="updateCardRequestCardUpdateOptions"></param>
    /// <returns></returns>
    public   UpdateCardResponse UpdateCard(string accessToken, 
        UpdateCardRequest updateCardRequest,
        UpdateCardRequest.UpdateCardRequestCardUpdateOptions updateCardRequestCardUpdateOptions = null
        )
    {
        UpdateCardHeaders updateCardHeaders = new UpdateCardHeaders();
        updateCardHeaders.XAcsDingtalkAccessToken = accessToken;
        updateCardRequest.CardUpdateOptions ??= new UpdateCardRequest.UpdateCardRequestCardUpdateOptions()
        {
            UpdateCardDataByKey = true,
            UpdatePrivateDataByKey = true
        };

        return DingtalkUtil.ExecuteAndCatchException(() => _client.UpdateCardWithOptions(updateCardRequest, updateCardHeaders, new RuntimeOptions()));
    }
}