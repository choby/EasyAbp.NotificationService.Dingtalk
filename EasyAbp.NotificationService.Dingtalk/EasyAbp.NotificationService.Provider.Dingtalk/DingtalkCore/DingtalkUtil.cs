using Tea;
using Volo.Abp;

namespace EasyAbp.NotificationService.Provider.Dingtalk.DingtalkCore;

public class DingtalkUtil
{
    public static T ExecuteAndCatchException<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch (TeaException err)
        {
            if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
            {
                // err 中含有 code 和 message 属性，可帮助开发定位问题
                throw new BusinessException(err.Code ,message: err.Message, innerException: err);
            }

            throw new BusinessException(message: err.Message, innerException: err);
        }
        catch (Exception _err)
        {
            TeaException err = new TeaException(new Dictionary<string, object>
            {
                { "message", _err.Message }
            });
            if (!AlibabaCloud.TeaUtil.Common.Empty(err.Code) && !AlibabaCloud.TeaUtil.Common.Empty(err.Message))
            {
                // err 中含有 code 和 message 属性，可帮助开发定位问题
                
                throw new BusinessException(err.Code ,message: err.Message, innerException: _err);
            }

            throw new BusinessException(message: err.Message, innerException: _err);
        }
    }
}