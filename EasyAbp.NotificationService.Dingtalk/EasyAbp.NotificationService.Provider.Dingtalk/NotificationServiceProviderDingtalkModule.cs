using EasyAbp.NotificationService.Options;
using EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.Dingtalk;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(NotificationServiceProviderDingtalkAbstractionsModule)
)]
public class NotificationServiceProviderDingtalkModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<NotificationServiceOptions>(options =>
        {
            options.Providers.AddProvider(new NotificationServiceProviderConfiguration(NotificationProviderDingtalkConsts.RobotInteractiveCardNotificationMethod, typeof(RobotInteractiveCardNotificationManager)));
        });
        
        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<Configuration>(configuration.GetSection("Dingtalk"));
        context.Services.AddTransient<IDingtalkOAuth, DingtalkOAuth>();
        context.Services.AddTransient<IDingtalkRobot, DingtalkRobot>();
        context.Services.AddTransient<IDingtalkCard, DingtalkCard>();
        context.Services.AddTransient<IDingtalkContact, DingtalkContact>();
    }
}