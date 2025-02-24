using System.Threading.Tasks;
using Abp.Application.Services;
using SchoolManagement.Sessions.Dto;

namespace SchoolManagement.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
