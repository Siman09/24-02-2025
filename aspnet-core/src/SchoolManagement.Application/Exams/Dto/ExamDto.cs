using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SchoolManagement.Exams.Dto
{
    public class ExamDto : AuditedEntityDto<int>
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int marks { get; set; }
        public string grade { get; set; }

    }
}
