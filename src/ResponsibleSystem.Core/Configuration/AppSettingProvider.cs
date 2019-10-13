using System.Collections.Generic;
using Abp.Configuration;

namespace ResponsibleSystem.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    AppConfig.SettingsNames.UiTheme,
                    AppConfig.Defaults.UiTheme,
                    scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, 
                    isVisibleToClients: true)
            };
        }
    }
}
