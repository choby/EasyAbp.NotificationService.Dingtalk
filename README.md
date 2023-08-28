# EasyAbp.NotificationService.Dingtalk
基于EasyAbp.NotificationService的钉钉机器人消息通知

使用方式：
- 安装EasyAbp

- 安装EasyAbp.NotificationService.Provider.Dingtalk
```shell
dotnet add package EasyAbp.NotificationService.Provider.Dingtalk
```

- 添加module依赖
```csharp
[DependsOn(
    typeof(NotificationServiceProviderDingtalkModule)
)]
```

- 配置钉钉,appsettings.json中添加：
```json
   "Dingtalk": {       
        "AppKey": "你的appkey",
        "AppSecret": "你的appsecret",
        "RedirectUri": "忽略",
        "WebHookAesKey": "忽略",
        "WebHookToken": "忽略",
      	"RobotCode": "你的钉钉机器人RobotCode"
    }
```

- 实现IDingtalkUserIdProvider
```csharp
[Dependency(ReplaceServices = true, TryRegister = true)]
public class DingtalkUserIdProvider : IDingtalkUserIdProvider , ITransientDependency
{
    

    public DingtalkUserIdProvider(r)
    {
        
    }

    public async Task<string> GetOrNullAsync(string appId, Guid userId)
    {
        //实现业务系统用户和钉钉用户的转换
    }

    public async Task<string> GetOrNullAsync(Guid userId)
    {
       // 实现业务系统用户和钉钉用户的转换
    }
}
```


- 发送消息, 实现工厂类并使用：
```csharp

public class YourNotificationFactory : NotificationFactory<YourData, CreateDingtalkRobotInteractiveCardNotificationEto>, ITransientDependency
{
    private ICurrentTenant _currentTenant;
    private readonly IDingtalkInteractiveCardNotificationDataModelJsonSerializer _jsonSerializer; 
    
   

    public SampleErrorDingtalkRobotInteractiveCardNotificationFactory(ICurrentTenant currentTenant, IDingtalkInteractiveCardNotificationDataModelJsonSerializer jsonSerializer)
    {
        _currentTenant = currentTenant;
        _jsonSerializer = jsonSerializer;       
    }

    public override async Task<CreateDingtalkRobotInteractiveCardNotificationEto> CreateAsync(YourData model, 
        IEnumerable<Guid> userIds)
    {
        var departments = await _openUserService.QueryDepartmentNamesAsync(new Guid[] { model.SampleErrorNotification.CreatorId!.Value });
        var eto = new CreateDingtalkRobotInteractiveCardNotificationEto(_currentTenant.Id.GetValueOrDefault(),
            userIds,
            new DingtalkRobotInteractiveCardDataModel()
            {
                CardBizId = "your notification id",
                CardData = new
                    {
                        config = new
                        {
                            autoLayout = true,
                            enableForward = true,
                        },
                        header = new
                        {
                            title = new
                            {
                                type = "text",
                                text = $"消息标题"
                            },
                            logo = "@lALPDfJ6V_FPDmvNAfTNAfQ"
                        },
                        contents = new List<object>()
                        {
                            new
                            {
                                type = "section",
                                fields = new
                                {
                                    list = new List<object>
                                    {
                                        new
                                        {
                                            type = "text",
                                            text = $"消息正文段落1",
                                        },
                                        new
                                        {
                                            type = "text",
                                            text = $"消息正文段落2",
                                        },
                                        new
                                        {
                                            type = "text",
                                            text = $"消息正文段落3",
                                        },
                                        new
                                        {

                                            type = "text",
                                            text = $"消息正文段落4",
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, _jsonSerializer);
        return eto;
    }
}

```

- 在业务层中调用YourNotificationFactory.CreateAsync()，得到eto并发布

  
通知效果：
![image](https://github.com/choby/EasyAbp.NotificationService.Dingtalk/assets/13461239/e1ed982d-752e-4f41-856c-2b880f80629d)





