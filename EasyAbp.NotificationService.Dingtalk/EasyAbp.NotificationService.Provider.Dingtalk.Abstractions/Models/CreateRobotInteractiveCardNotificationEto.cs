using EasyAbp.NotificationService.Notifications;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

/// <summary>
/// 机器人互动卡片通知(普通版)
/// </summary>
public class CreateRobotInteractiveCardNotificationEto : RobotInteractiveCardNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; }
   
   
    
   

    public CreateRobotInteractiveCardNotificationEto(Guid? tenantId ,
        IEnumerable<Guid> userIds, 
        RobotInteractiveCardDataModel dataModel
        , IRobotInteractiveCardNotificationDataModelJsonSerializer robotInteractiveCardNotificationDataModelJsonSerializer
        )
        : base(NotificationProviderDingtalkConsts.RobotInteractiveCardNotificationMethod, userIds)
    {
        TenantId = tenantId;
        this.SetDataModel(dataModel,robotInteractiveCardNotificationDataModelJsonSerializer);
    }

    
}