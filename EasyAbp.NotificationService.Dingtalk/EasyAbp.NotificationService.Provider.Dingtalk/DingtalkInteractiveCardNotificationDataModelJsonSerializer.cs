using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class DingtalkInteractiveCardNotificationDataModelJsonSerializer : IDingtalkInteractiveCardNotificationDataModelJsonSerializer,
    ITransientDependency
{
    private IJsonSerializer _jsonSerializer;

    public DingtalkInteractiveCardNotificationDataModelJsonSerializer(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual string Serialize(DingtalkRobotInteractiveCardDataModel dataModel)
    {
        return _jsonSerializer.Serialize(dataModel);
    }

    public virtual DingtalkRobotInteractiveCardDataModel Deserialize(string jsonString)
    {
        return _jsonSerializer.Deserialize<DingtalkRobotInteractiveCardDataModel>(jsonString);
    }
}