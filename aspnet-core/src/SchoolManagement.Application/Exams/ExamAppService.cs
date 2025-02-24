using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq;
using Abp.Linq.Extensions;
using SchoolManagement.Authorization;
using SchoolManagement.Classes;
using SchoolManagement.Classes.Dto;
using SchoolManagement.Exam;
using SchoolManagement.Exams.Dto;
using SchoolManagement.Students;
using SchoolManagement.Students.Dto;
using SchoolManagement.Teachers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchoolManagement.Exams
{
    [AbpAuthorize(PermissionNames.pages_School_Management_project)]
    [AbpAuthorize(PermissionNames.pages_ExamManagement_permission)]
    public class ExamAppService:SchoolManagementAppServiceBase,IExamAppService
    {
        private readonly IRepository<Exam.Exam> repository;
        private readonly IRepository<Student> StudentAppService;
        private readonly IRepository<Teacher> TeacherAppService;

        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        public ExamAppService(IRepository<Exam.Exam> _repository,
                              IRepository<Student> _StudentAppService,
                              IRepository<Teacher> _TeacherAppService)
        {
            repository = _repository;
            StudentAppService = _StudentAppService;
            TeacherAppService = _TeacherAppService;
        }

        [AbpAuthorize(PermissionNames.pages_CreateExam_permission)] 
        public async Task CreateAsync(CreateExamDto input)
        {
            await repository.InsertAsync(ObjectMapper.Map<Exam.Exam>(input));
        }

        [AbpAuthorize(PermissionNames.pages_DeleteExam_permission)]
        public async Task DeleteAsync(int input)
        {
            await repository.DeleteAsync(input);
        }

        public async Task<PagedResultDto<ExamDto>> GetAllAsync(PagedExamResultRequestDto input)
        {
            var ScoreData = repository.GetAll();
            var totalCount = AsyncQueryableExecuter.CountAsync(ScoreData);
            var student = StudentAppService.GetAll();
            var Teacher = TeacherAppService.GetAll();

            var joinQuery = from marksdata in ScoreData
                            join studentdata in student
                            on marksdata.StudentId equals studentdata.Id
                            join teacherdata in Teacher
                            on marksdata.TeacherId equals teacherdata.Id
                            select new ExamDto
                              { 
                                Id = marksdata.Id,
                                grade = GetGrade(marksdata.marks),
                                TeacherId = marksdata.TeacherId,
                                TeacherName = teacherdata.name,
                                StudentId = marksdata.StudentId,
                                StudentName = studentdata.Name,
                              };
           var query = CreateFilterQuery(input, joinQuery);
               query = ApplyPaging(query, input);
            return new PagedResultDto<ExamDto>(await totalCount, query.ToList());
        }
        public static string GetGrade(int score)
        {
            if (score < 0 || score > 100)
            {
                return "Invalid score.";
            }
            if (score >= 90) return "A+";
            if (score >= 80) return "A";
            if (score >= 70) return "B";
            if (score >= 60) return "C";
            if (score >= 50) return "D";

            return "F"; 
        }

        public async Task<ExamDto> GetAsync(int input)
        {
            return MapToEntityDto(await repository.FirstOrDefaultAsync(x => x.Id == input));
        }


        [AbpAuthorize(PermissionNames.pages_EditExam_permission)]
        public async Task UpdateAsync(CreateExamDto input)
        {
            await repository.UpdateAsync(ObjectMapper.Map<Exam.Exam>(input));
        }

        protected IQueryable<ExamDto> CreateFilterQuery(PagedExamResultRequestDto input,IQueryable<ExamDto> joinQuery)
        {
            return joinQuery
                   .Where(x => string.IsNullOrWhiteSpace(input.Keyword) ||
                          x.StudentName.Contains(input.Keyword) ||
                          x.TeacherName.Contains(input.Keyword));

        }
        protected IQueryable<ExamDto> ApplyPaging(IQueryable<ExamDto> query, PagedExamResultRequestDto input)
        {
            //Try to use paging if available
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                var data = query.PageBy(pagedInput);
                return data;
            }
            //Try to limit query result if available
            //var limitedInput = input as ILimitedResultRequest;
            //if (limitedInput != null)
            //{
            //    return query.Take(limitedInput.MaxResultCount);
            //}
            //No paging
            return query;
        }
        protected ExamDto MapToEntityDto(Exam.Exam entity)
        {
            return ObjectMapper.Map<ExamDto>(entity);
        }
    }
}
