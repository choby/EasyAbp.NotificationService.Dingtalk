using EasyAbp.NotificationService.Notifications;
using Volo.Abp.Data;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public static class CreateNotificationInfoModelExtensions
    {
        public static void SetDataModel(this CreateNotificationInfoModel model,
            DingtalkRobotInteractiveCardDataModel dataModel,
            IDingtalkInteractiveCardNotificationDataModelJsonSerializer dingtalkInteractiveCardNotificationDataModelJsonSerializer)
        {
            model.SetJsonDataModel(dingtalkInteractiveCardNotificationDataModelJsonSerializer.Serialize(dataModel));
        }

        public static DingtalkRobotInteractiveCardDataModel GetDataModel(this CreateNotificationInfoModel model,
            IDingtalkInteractiveCardNotificationDataModelJsonSerializer dingtalkInteractiveCardNotificationDataModelJsonSerializer)
        {
            return dingtalkInteractiveCardNotificationDataModelJsonSerializer.Deserialize(model.GetJsonDataModel());
        }

        public static void SetJsonDataModel(this CreateNotificationInfoModel model, string jsonDataModel)
        {
            model.SetProperty(nameof(CreateDingtalkRobotInteractiveCardNotificationEto.JsonDataModel), jsonDataModel);
        }

        public static string GetJsonDataModel(this CreateNotificationInfoModel model)
        {
            return (string)model.GetProperty(nameof(CreateDingtalkRobotInteractiveCardNotificationEto.JsonDataModel));
        }
    }
}
