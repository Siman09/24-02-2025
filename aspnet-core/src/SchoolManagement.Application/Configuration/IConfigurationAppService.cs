using System.Threading.Tasks;
using SchoolManagement.Configuration.Dto;

namespace SchoolManagement.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
