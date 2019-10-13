using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ResponsibleSystem.Configuration;
using ResponsibleSystem.Shared.Configuration.Dto;

namespace ResponsibleSystem.Shared.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ResponsibleSystemAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                AppConfig.SettingsNames.UiTheme, 
                input.Theme);
        }
    }
}
