using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SchoolManagement.Authorization
{
    public class SchoolManagementAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
          context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
          context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
          context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
          context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

             context.CreatePermission(PermissionNames.pages_School_Management_project, L("SchoolManagement"));
             var teacherManagement = context.CreatePermission(PermissionNames.pages_TeacherManagement_permission, L("TeacherManagement"));
                   teacherManagement.CreateChildPermission(PermissionNames.pages_CreateTeacher_permission, L("CreateTeacher"));
                   teacherManagement.CreateChildPermission(PermissionNames.pages_EditTeacher_permission, L("EditTeacher"));
                   teacherManagement.CreateChildPermission(PermissionNames.Pages_DeleteTeacher_Permission, L("DeleteTeacher"));

             var examManagement = context.CreatePermission(PermissionNames.pages_ExamManagement_permission, L("ExamManagement"));
                   examManagement.CreateChildPermission(PermissionNames.pages_CreateExam_permission, L("CreateExam"));
                   examManagement.CreateChildPermission(PermissionNames.pages_EditExam_permission, L("EditExam"));
                   examManagement.CreateChildPermission(PermissionNames.pages_DeleteExam_permission, L("DeleteExam"));

             var studentManagement = context.CreatePermission(PermissionNames.pages_StudentManagement_permission, L("StudentManagement"));
                   studentManagement.CreateChildPermission(PermissionNames.pages_CreateStudent_permission, L("CreateStudent"));
                   studentManagement.CreateChildPermission(PermissionNames.pages_EditStudent_Permission, L("EditStudent"));
                   studentManagement.CreateChildPermission(PermissionNames.pages_DeleteStudent_Permission, L("DeleteStudent"));

             var classManagement = context.CreatePermission(PermissionNames.pages_ClassManagement_permission, L("ClassManagement"));
                   classManagement.CreateChildPermission(PermissionNames.pages_CreateClass_permission, L("CreateClass"));
                   classManagement.CreateChildPermission(PermissionNames.pages_EditClass_permission, L("EditClass"));
                   classManagement.CreateChildPermission(PermissionNames.pages_DeleteClass_permission, L("DeleteClass"));
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SchoolManagementConsts.LocalizationSourceName);
        }
    }
}
