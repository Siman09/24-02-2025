using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using SchoolManagement.Classes;

namespace SchoolManagement.Teachers
{
    public class Teacher:AuditedEntity<int>
    {
        public string name { get; set; }
        public string age { get; set; }
        public string Email { get; set; }
        public GenderEnum gender { get; set; }
        public bool maritalStatus { get; set; }
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        
        public StudentClass Classes { get; set; }
    }

    public enum GenderEnum
    {
        Male = 1,
        Female = 2,
        Other = 3,
    }
}
