using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using SchoolManagement.Configuration.Dto;

namespace SchoolManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SchoolManagementAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
