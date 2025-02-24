using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SchoolManagement.Teachers;

namespace SchoolManagement.Students.Dto
{
    public class StudentDto:AuditedEntityDto<int>
    {
        public string Name { get; set; }
        public string RollNo { get; set; }
        public string age { get; set; }
        public bool maritalStatus { get; set; }
        public GenderEnum gender { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }

    }
}
