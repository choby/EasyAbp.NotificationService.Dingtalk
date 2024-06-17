using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class InteractiveCardNotificationDataModelJsonSerializer : IInteractiveCardNotificationDataModelJsonSerializer,
    ITransientDependency
{
    private IJsonSerializer _jsonSerializer;

    public InteractiveCardNotificationDataModelJsonSerializer(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual string Serialize(InteractiveCardDataModel dataModel)
    {
        return _jsonSerializer.Serialize(dataModel);
    }

    public virtual InteractiveCardDataModel Deserialize(string jsonString)
    {
        return _jsonSerializer.Deserialize<InteractiveCardDataModel>(jsonString);
    }
}