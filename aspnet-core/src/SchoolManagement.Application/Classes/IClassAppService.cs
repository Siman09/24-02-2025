using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Classes.Dto;
using SchoolManagement.Students.Dto;

namespace SchoolManagement.Classes
{
    public interface IClassAppService :IApplicationService
    {
        Task CreateAsync(CreateClassDto input);
        Task DeleteAsync(EntityDto<int> input);
        Task<PagedResultDto<ClassDto>> GetAllAsync(PagedStudentResultRequestDto input);
        Task<ClassDto> GetAsync(int input);
        Task UpdateAsync(ClassDto input);

    }
}
