using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public class InteractiveCardDingtalkNotificationSendingJob : IAsyncBackgroundJob<InteractiveCardNotificationSendingJobArgs>, ITransientDependency
    {
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ICurrentTenant _currentTenant;

        private readonly InteractiveCardNotificationManager _interactiveCardNotificationManager;
        //private readonly IRepository<NotificationInfo, Guid> _notificationInfoRepository;
        //private readonly IRepository<Notification, Guid> _notificationRepository;

        public InteractiveCardDingtalkNotificationSendingJob(
            IClock clock,
            //IRepository<NotificationInfo, Guid> notificationInfoRepository,
            //IRepository<Notification, Guid> notificationRepository, 
            INotificationInfoRepository notificationInfoRepository,
            INotificationRepository notificationRepository,
            ICurrentTenant currentTenant, 
            InteractiveCardNotificationManager interactiveCardNotificationManager)
        {
           
            _notificationInfoRepository = notificationInfoRepository;
            _notificationRepository = notificationRepository;
            _currentTenant = currentTenant;
            _interactiveCardNotificationManager = interactiveCardNotificationManager;
        }

        
        public async Task ExecuteAsync(InteractiveCardNotificationSendingJobArgs args)
        {
            using var changeTenant = _currentTenant.Change(args.TenantId);

            var notification = await _notificationRepository.GetAsync(args.NotificationId);
            var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

            await _interactiveCardNotificationManager.SendNotificationsAsync(new List<Notification> { notification }, notificationInfo);
        }
    }
}
