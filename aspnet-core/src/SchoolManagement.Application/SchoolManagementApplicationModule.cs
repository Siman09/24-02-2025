using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SchoolManagement.Authorization;

namespace SchoolManagement
{
    [DependsOn(
        typeof(SchoolManagementCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SchoolManagementApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SchoolManagementAuthorizationProvider>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.ConfigureBasicInfoMappings);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SchoolManagementApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)

            );
        }
    }
}
