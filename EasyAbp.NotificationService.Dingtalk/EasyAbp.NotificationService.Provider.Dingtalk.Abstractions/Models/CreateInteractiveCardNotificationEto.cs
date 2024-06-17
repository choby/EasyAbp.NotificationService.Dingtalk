using EasyAbp.NotificationService.Notifications;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

/// <summary>
/// 互动卡片通知(高级版)
/// </summary>
public class CreateInteractiveCardNotificationEto : RobotInteractiveCardNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; }
   
   
    public string JsonDataModel
    {
        get => this.GetJsonDataModel();
        set => this.SetJsonDataModel(value);
    }
   

    public CreateInteractiveCardNotificationEto(Guid? tenantId ,
        IEnumerable<Guid> userIds, 
        InteractiveCardDataModel dataModel,
        IInteractiveCardNotificationDataModelJsonSerializer interactiveCardNotificationDataModelJsonSerializer
        )
        : base(NotificationProviderDingtalkConsts.InteractiveCardNotificationMethod, userIds)
    {
        TenantId = tenantId;
        this.SetDataModel(dataModel,interactiveCardNotificationDataModelJsonSerializer);
    }

    
}