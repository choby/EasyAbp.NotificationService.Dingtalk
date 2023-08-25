using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public class DingtalkInteractiveCardDingtalkNotificationSendingJob : IAsyncBackgroundJob<DingtalkInteractiveCardNotificationSendingJobArgs>, ITransientDependency
    {
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentTenant _currentTenant;

        private readonly DingtalkRobotInteractiveCardNotificationManager _dingtalkRobotInteractiveCardNotificationManager;
        //private readonly IRepository<NotificationInfo, Guid> _notificationInfoRepository;
        //private readonly IRepository<Notification, Guid> _notificationRepository;

        public DingtalkInteractiveCardDingtalkNotificationSendingJob(
            IClock clock,
            //IRepository<NotificationInfo, Guid> notificationInfoRepository,
            //IRepository<Notification, Guid> notificationRepository, 
            INotificationInfoRepository notificationInfoRepository,
            INotificationRepository notificationRepository,
            ICurrentTenant currentTenant, 
            DingtalkRobotInteractiveCardNotificationManager dingtalkRobotInteractiveCardNotificationManager)
        {
           
            _notificationInfoRepository = notificationInfoRepository;
            _notificationRepository = notificationRepository;
            _currentTenant = currentTenant;
            _dingtalkRobotInteractiveCardNotificationManager = dingtalkRobotInteractiveCardNotificationManager;
        }

        
        public async Task ExecuteAsync(DingtalkInteractiveCardNotificationSendingJobArgs args)
        {
            using var changeTenant = _currentTenant.Change(args.TenantId);

            var notification = await _notificationRepository.GetAsync(args.NotificationId);
            var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

            await _dingtalkRobotInteractiveCardNotificationManager.SendNotificationsAsync(new List<Notification> { notification }, notificationInfo);
        }
    }
}
