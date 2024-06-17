using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class RobotInteractiveCardNotificationDataModelJsonSerializer : IRobotInteractiveCardNotificationDataModelJsonSerializer,
    ITransientDependency
{
    private IJsonSerializer _jsonSerializer;

    public RobotInteractiveCardNotificationDataModelJsonSerializer(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual string Serialize(RobotInteractiveCardDataModel dataModel)
    {
        return _jsonSerializer.Serialize(dataModel);
    }

    public virtual RobotInteractiveCardDataModel Deserialize(string jsonString)
    {
        return _jsonSerializer.Deserialize<RobotInteractiveCardDataModel>(jsonString);
    }
}