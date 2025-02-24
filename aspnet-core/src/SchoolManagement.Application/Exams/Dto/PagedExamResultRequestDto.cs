using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SchoolManagement.Exams.Dto
{
    public class PagedExamResultRequestDto:PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
