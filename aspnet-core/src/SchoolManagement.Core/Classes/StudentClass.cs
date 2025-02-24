using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace SchoolManagement.Classes
{
    public class StudentClass:AuditedEntity<int>
    {
        public string title { get; set; }
    }
}
