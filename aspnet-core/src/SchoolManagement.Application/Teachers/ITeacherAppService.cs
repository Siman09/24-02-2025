using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SchoolManagement.Students.Dto;
using SchoolManagement.Teachers.Dto;

namespace SchoolManagement.Teachers
{
    public interface ITeacherAppService : IApplicationService
    {
        Task CreateAsync(CreateTeacherDto input);
        Task DeleteAsync(EntityDto<int> input);
        Task<TeacherDto> GetAsync(int input);
        Task UpdateAsync(TeacherDto input);
        Task<PagedResultDto<TeacherDto>> GetAllAsync(PagedStudentResultRequestDto input);
    }
}
