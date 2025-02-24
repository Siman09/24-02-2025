using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace SchoolManagement.Controllers
{
    public abstract class SchoolManagementControllerBase: AbpController
    {
        protected SchoolManagementControllerBase()
        {
            LocalizationSourceName = SchoolManagementConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
