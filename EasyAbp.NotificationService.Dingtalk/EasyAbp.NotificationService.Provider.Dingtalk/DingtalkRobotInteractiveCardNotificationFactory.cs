using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{

    public class DingtalkRobotInteractiveCardNotificationFactory : NotificationFactory<DingtalkRobotInteractiveCardDataModel, CreateDingtalkRobotInteractiveCardNotificationEto>, ITransientDependency
    {
         private readonly IDingtalkInteractiveCardNotificationDataModelJsonSerializer _jsonSerializer; //使用tdl内容模板
        public DingtalkRobotInteractiveCardNotificationFactory(IDingtalkInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
        public override Task<CreateDingtalkRobotInteractiveCardNotificationEto> CreateAsync(DingtalkRobotInteractiveCardDataModel model, IEnumerable<Guid> userIds)
        {
            return Task.FromResult(new CreateDingtalkRobotInteractiveCardNotificationEto(CurrentTenant.Id, userIds, model, _jsonSerializer));
        }
    }
}
