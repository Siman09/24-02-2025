using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SchoolManagement.Classes;

namespace SchoolManagement.Teachers.Dto
{
    public class CreateTeacherDto:AuditedEntityDto<int>
    {
        public string name { get; set; }
        public string age { get; set; }
        public string Email { get; set; }
        public GenderEnum gender { get; set; }
        public bool maritalStatus { get; set; }
        public int ClassId { get; set; }
    }
}
