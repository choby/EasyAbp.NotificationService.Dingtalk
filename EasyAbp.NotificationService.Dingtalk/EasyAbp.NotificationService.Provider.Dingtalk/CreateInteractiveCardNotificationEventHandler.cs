using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public class CreateInteractiveCardNotificationEventHandler : ILocalEventHandler<CreateInteractiveCardNotificationEto>, ILocalEventHandler<IEnumerable<CreateInteractiveCardNotificationEto>>,ITransientDependency
    {
        private readonly InteractiveCardNotificationManager _interactiveCardNotificationManager;
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly INotificationRepository _notificationRepository;
        public CreateInteractiveCardNotificationEventHandler(INotificationInfoRepository notificationInfoRepository, 
            InteractiveCardNotificationManager interactiveCardNotificationManager, 
            INotificationRepository notificationRepository)
        {
            _notificationInfoRepository = notificationInfoRepository;
            _interactiveCardNotificationManager = interactiveCardNotificationManager;
            _notificationRepository = notificationRepository;
        }

        public virtual async Task HandleEventAsync(CreateInteractiveCardNotificationEto eventData)
        {
            var result = await _interactiveCardNotificationManager.CreateAsync(eventData);

            await _notificationInfoRepository.InsertAsync(result.Item2, true);
            foreach (var notification in result.Item1)
            {
                await _notificationRepository.InsertAsync(notification, true);
            }

        }

        public virtual async Task HandleEventAsync(IEnumerable<CreateInteractiveCardNotificationEto> eventData)
        {
            foreach (var item in eventData)
            {
                var result = await _interactiveCardNotificationManager.CreateAsync(item);
                
                await _notificationInfoRepository.InsertAsync(result.Item2, true);
                foreach (var notification in result.Item1)
                {
                    await _notificationRepository.InsertAsync(notification, true);
                }
            }
        }
    }
}
