
namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IInteractiveCardNotificationDataModelJsonSerializer
{
    string Serialize(InteractiveCardDataModel dataModel);


    InteractiveCardDataModel Deserialize(string jsonString);

}