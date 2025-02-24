using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SchoolManagement.Teachers.Dto
{
    public class TeacherDto:AuditedEntityDto<int>
    {
        public string name { get; set; }
        public string age { get; set; }
        public string Email { get; set; }
        public GenderEnum gender { get; set; }
        public bool maritalStatus { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }

    }
}
