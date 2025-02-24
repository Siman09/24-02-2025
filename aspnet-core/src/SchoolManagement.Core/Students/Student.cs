using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using SchoolManagement.Classes;
using SchoolManagement.Teachers;

namespace SchoolManagement.Students
{
    public class Student:AuditedEntity<int>
    {
        public string Name { get; set; }
        public string RollNo { get; set; }
        public string age { get; set; }
        public GenderEnum gender { get; set; }
        public bool maritalStatus { get; set; }

        [ForeignKey("ClassID")]
        public int ClassID { get; set; }

        public StudentClass Classes { get; set; }
    } 
}
