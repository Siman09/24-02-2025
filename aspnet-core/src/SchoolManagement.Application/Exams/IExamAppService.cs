using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SchoolManagement.Classes.Dto;
using SchoolManagement.Exams.Dto;
using SchoolManagement.Students.Dto;

namespace SchoolManagement.Exams
{
    public interface IExamAppService:IApplicationService
    {
        Task CreateAsync(CreateExamDto input);
        Task DeleteAsync(int input);
        Task<PagedResultDto<ExamDto>> GetAllAsync(PagedExamResultRequestDto input);
        Task<ExamDto> GetAsync(int input);
        Task UpdateAsync(CreateExamDto input);
    }
}
