using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public class RobotInteractiveCardNotificationSendingJobArgs : IMultiTenant
    {
        public RobotInteractiveCardNotificationSendingJobArgs(Guid? tenantId, Guid notificationId)
        {
            TenantId = tenantId;
            NotificationId = notificationId;
        }
        public Guid NotificationId { get; set; }
        public Guid? TenantId { get; }
    }
}
