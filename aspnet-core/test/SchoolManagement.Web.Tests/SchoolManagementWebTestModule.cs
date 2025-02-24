using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SchoolManagement.EntityFrameworkCore;
using SchoolManagement.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace SchoolManagement.Web.Tests
{
    [DependsOn(
        typeof(SchoolManagementWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class SchoolManagementWebTestModule : AbpModule
    {
        public SchoolManagementWebTestModule(SchoolManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SchoolManagementWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(SchoolManagementWebMvcModule).Assembly);
        }
    }
}