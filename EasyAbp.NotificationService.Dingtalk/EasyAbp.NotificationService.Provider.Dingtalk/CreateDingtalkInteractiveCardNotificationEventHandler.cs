using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public class CreateDingtalkInteractiveCardNotificationEventHandler : ILocalEventHandler<CreateDingtalkRobotInteractiveCardNotificationEto>, ITransientDependency
    {
        private readonly DingtalkRobotInteractiveCardNotificationManager _dingtalkRobotInteractiveCardNotificationManager;
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly INotificationRepository _notificationRepository;
        public CreateDingtalkInteractiveCardNotificationEventHandler(   INotificationInfoRepository notificationInfoRepository, 
            DingtalkRobotInteractiveCardNotificationManager dingtalkRobotInteractiveCardNotificationManager, 
            INotificationRepository notificationRepository)
        {
            _notificationInfoRepository = notificationInfoRepository;
            _dingtalkRobotInteractiveCardNotificationManager = dingtalkRobotInteractiveCardNotificationManager;
            _notificationRepository = notificationRepository;
        }

        public virtual async Task HandleEventAsync(CreateDingtalkRobotInteractiveCardNotificationEto eventData)
        {
            var result = await _dingtalkRobotInteractiveCardNotificationManager.CreateAsync(eventData);
            
                
                await _notificationInfoRepository.InsertAsync(result.Item2, true);
                foreach (var notification in result.Item1)
                {
                    await _notificationRepository.InsertAsync(notification, true);
                }
                
        }
       
    }
}
