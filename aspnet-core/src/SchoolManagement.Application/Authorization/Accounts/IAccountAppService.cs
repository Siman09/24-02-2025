using System.Threading.Tasks;
using Abp.Application.Services;
using SchoolManagement.Authorization.Accounts.Dto;

namespace SchoolManagement.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
