using Abp.Application.Services;
using SchoolManagement.MultiTenancy.Dto;

namespace SchoolManagement.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

