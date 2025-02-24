using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SchoolManagement.Students;
using SchoolManagement.Teachers;

namespace SchoolManagement.Exams.Dto
{
    public class CreateExamDto : AuditedEntityDto<int>
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set;}
        public int marks { get; set; }
    }
}
