using EasyAbp.NotificationService.Notifications;
using Volo.Abp.Data;

namespace EasyAbp.NotificationService.Provider.Dingtalk
{
    public static class CreateNotificationInfoModelExtensions
    {
        #region 互动卡片(普通版)
        public static void SetDataModel(this CreateNotificationInfoModel model,
            RobotInteractiveCardDataModel dataModel,
            IRobotInteractiveCardNotificationDataModelJsonSerializer robotInteractiveCardNotificationDataModelJsonSerializer)
        {
            model.SetJsonDataModel(robotInteractiveCardNotificationDataModelJsonSerializer.Serialize(dataModel));
        }

        public static RobotInteractiveCardDataModel GetDataModel(this CreateNotificationInfoModel model,
            IRobotInteractiveCardNotificationDataModelJsonSerializer robotInteractiveCardNotificationDataModelJsonSerializer)
        {
            return robotInteractiveCardNotificationDataModelJsonSerializer.Deserialize(model.GetJsonDataModel());
        }
        
        #endregion
        
        #region 互动卡片(高级版)
        public static void SetDataModel(this CreateNotificationInfoModel model,
            InteractiveCardDataModel dataModel,
            IInteractiveCardNotificationDataModelJsonSerializer interactiveCardNotificationDataModelJsonSerializer)
        {
            model.SetJsonDataModel(interactiveCardNotificationDataModelJsonSerializer.Serialize(dataModel));
        }

        public static InteractiveCardDataModel GetDataModel(this CreateNotificationInfoModel model,
            IInteractiveCardNotificationDataModelJsonSerializer interactiveCardNotificationDataModelJsonSerializer)
        {
            return interactiveCardNotificationDataModelJsonSerializer.Deserialize(model.GetJsonDataModel());
        }
        
        #endregion

        public static void SetJsonDataModel(this CreateNotificationInfoModel model, string jsonDataModel)
        {
            model.SetProperty(nameof(RobotInteractiveCardNotificationInfoModel.JsonDataModel), jsonDataModel);
        }

        public static string GetJsonDataModel(this CreateNotificationInfoModel model)
        {
            return (string)model.GetProperty(nameof(RobotInteractiveCardNotificationInfoModel.JsonDataModel));
        }
    }
}
