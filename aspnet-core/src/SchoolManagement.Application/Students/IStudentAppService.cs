using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SchoolManagement.Students.Dto;
using SchoolManagement.Users.Dto;

namespace SchoolManagement.Students
{
    public interface IStudentAppService: IAsyncCrudAppService<StudentDto, int, PagedStudentResultRequestDto, CreateStudentDto, StudentDto>
    {
        
    }
}
