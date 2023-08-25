

namespace EasyAbp.NotificationService.Provider.Dingtalk;

public class DingtalkRobotInteractiveCardDataModel
{
    public string? AppId { get; set; }
    public string CardBizId { get; set; }
    public object CardData { get; set; }
}