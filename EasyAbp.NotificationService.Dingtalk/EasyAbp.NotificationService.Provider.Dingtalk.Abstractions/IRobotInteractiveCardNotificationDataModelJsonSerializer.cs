
namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IRobotInteractiveCardNotificationDataModelJsonSerializer
{
    string Serialize(RobotInteractiveCardDataModel dataModel);


    RobotInteractiveCardDataModel Deserialize(string jsonString);

}