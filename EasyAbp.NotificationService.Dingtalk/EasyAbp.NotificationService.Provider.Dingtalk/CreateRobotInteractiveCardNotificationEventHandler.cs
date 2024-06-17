using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public class CreateRobotInteractiveCardNotificationEventHandler : ILocalEventHandler<CreateRobotInteractiveCardNotificationEto>, ITransientDependency
    {
        private readonly RobotInteractiveCardNotificationManager _robotInteractiveCardNotificationManager;
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly INotificationRepository _notificationRepository;
        public CreateRobotInteractiveCardNotificationEventHandler(INotificationInfoRepository notificationInfoRepository, 
            RobotInteractiveCardNotificationManager robotInteractiveCardNotificationManager, 
            INotificationRepository notificationRepository)
        {
            _notificationInfoRepository = notificationInfoRepository;
            _robotInteractiveCardNotificationManager = robotInteractiveCardNotificationManager;
            _notificationRepository = notificationRepository;
        }

        public virtual async Task HandleEventAsync(CreateRobotInteractiveCardNotificationEto eventData)
        {
            var result = await _robotInteractiveCardNotificationManager.CreateAsync(eventData);
            
                
                await _notificationInfoRepository.InsertAsync(result.Item2, true);
                foreach (var notification in result.Item1)
                {
                    await _notificationRepository.InsertAsync(notification, true);
                }
                
        }
       
    }
}
