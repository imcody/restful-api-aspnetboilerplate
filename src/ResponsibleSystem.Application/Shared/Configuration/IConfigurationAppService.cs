using System.Threading.Tasks;
using ResponsibleSystem.Shared.Configuration.Dto;

namespace ResponsibleSystem.Shared.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
