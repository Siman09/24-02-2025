using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SchoolManagement.Classes.Dto
{
    public class CreateClassDto : AuditedEntityDto<int>
    {
        public string title { get; set; }
    }
}
