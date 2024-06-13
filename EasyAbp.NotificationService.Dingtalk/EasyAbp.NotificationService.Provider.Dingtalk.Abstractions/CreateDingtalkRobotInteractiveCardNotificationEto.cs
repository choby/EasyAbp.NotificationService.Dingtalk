using EasyAbp.NotificationService.Notifications;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

/// <summary>
/// 机器人互动卡片通知(普通版)
/// </summary>
public class CreateDingtalkRobotInteractiveCardNotificationEto : CreateNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; }
   
   
    public string JsonDataModel
    {
        get => this.GetJsonDataModel();
        set => this.SetJsonDataModel(value);
    }
   

    public CreateDingtalkRobotInteractiveCardNotificationEto(Guid? tenantId ,
        IEnumerable<Guid> userIds, 
        DingtalkRobotInteractiveCardDataModel dataModel
        , IDingtalkInteractiveCardNotificationDataModelJsonSerializer dingtalkInteractiveCardNotificationDataModelJsonSerializer
        )
        : base(NotificationProviderDingtalkConsts.RobotInteractiveCardNotificationMethod, userIds)
    {
        TenantId = tenantId;
        this.SetDataModel(dataModel,dingtalkInteractiveCardNotificationDataModelJsonSerializer);
    }

    
}