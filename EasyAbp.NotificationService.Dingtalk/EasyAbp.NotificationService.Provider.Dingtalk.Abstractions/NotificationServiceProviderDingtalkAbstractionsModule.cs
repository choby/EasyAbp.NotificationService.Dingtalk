using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

[DependsOn(
    typeof(NotificationServiceDomainSharedModule)
)]
public class NotificationServiceProviderDingtalkAbstractionsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
       
    }
}