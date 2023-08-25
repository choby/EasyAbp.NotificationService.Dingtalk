
namespace EasyAbp.NotificationService.Provider.Dingtalk;

public interface IDingtalkInteractiveCardNotificationDataModelJsonSerializer
{
    string Serialize(DingtalkRobotInteractiveCardDataModel dataModel);


    DingtalkRobotInteractiveCardDataModel Deserialize(string jsonString);

}