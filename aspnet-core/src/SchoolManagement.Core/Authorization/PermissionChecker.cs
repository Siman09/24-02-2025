using Abp.Authorization;
using SchoolManagement.Authorization.Roles;
using SchoolManagement.Authorization.Users;

namespace SchoolManagement.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
