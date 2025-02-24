using System.Collections.Generic;

namespace SchoolManagement.Roles.Dto
{
    public class FlatPermissionDto
    {
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public string Description { get; set; }
        public List<PermissionDto> Children { get; set; } = new List<PermissionDto>();

    }
}