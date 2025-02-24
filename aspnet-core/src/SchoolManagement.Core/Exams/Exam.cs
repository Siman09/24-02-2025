using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using SchoolManagement.Students;
using SchoolManagement.Teachers;

namespace SchoolManagement.Exam
{
    public class Exam:AuditedEntity<int>
    {
        [ForeignKey("TeacherId")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public Student students { get; set; }
        public int marks { get; set; }
    }
}
