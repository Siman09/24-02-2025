using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq;
using Abp.Linq.Extensions;
using SchoolManagement.Classes.Dto;
using SchoolManagement.Classes;
using SchoolManagement.Students.Dto;
using SchoolManagement.Teachers.Dto;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using SchoolManagement.Authorization;
using SchoolManagement.Repostory.Interface;

namespace SchoolManagement.Teachers
{
    [AbpAuthorize(PermissionNames.pages_School_Management_project)]
    [AbpAuthorize(PermissionNames.pages_TeacherManagement_permission)]
    public class TeacherAppService:SchoolManagementAppServiceBase,ITeacherAppService
    {
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }

        private readonly IRepository<Teacher> repository;
        private readonly IRepository<StudentClass> ClassAppService;
        private readonly IEmailSend emailSend;

        public TeacherAppService(IRepository<Teacher> _repository,
                                 IRepository<StudentClass> _ClassAppService,
                                 IEmailSend _emailSend
            )
        {
            repository = _repository;
            ClassAppService = _ClassAppService;
            emailSend = _emailSend;
        }

        [AbpAuthorize(PermissionNames.pages_CreateTeacher_permission)]
        public async Task CreateAsync(CreateTeacherDto input)
        {
            var checker = repository.FirstOrDefault(x => x.ClassId == input.ClassId);
            if (checker == null)
            {
                await repository.InsertAsync(ObjectMapper.Map<Teacher>(input));
                bool checkEmail = await emailSend.EmailSendAsync(input.Email, "Welcome to New Company", input.name+" "+"Thank you for joining us! We're excited to have you on board. You can now explore all the amazing features we offer.");
                if (checkEmail)
                {
                    Console.WriteLine("Email is Send Successfully");
                }

            }
            else
                throw new UserFriendlyException("This Class is Already assigned");

        }
        [AbpAuthorize(PermissionNames.Pages_DeleteTeacher_Permission)]
        public async Task DeleteAsync(EntityDto<int> input)
        {
            await repository.DeleteAsync(x=>x.Id==input.Id);
        }
        public async Task<TeacherDto> GetAsync(int input)
        {
            var data = await repository.FirstOrDefaultAsync(x => x.Id == input);
            return MapToEntityDto(data);
        }

        [AbpAuthorize(PermissionNames.pages_EditTeacher_permission)]
        public async Task UpdateAsync(TeacherDto input)
        {
            var checker = repository.FirstOrDefault(x => x.ClassId == input.ClassId);
            if (checker == null || checker.Id == input.Id)
                await repository.UpdateAsync(ObjectMapper.Map(input,checker));
            else
                throw new UserFriendlyException("This Class is Already assigned");

        }

        

        protected IQueryable<Teacher> CreateFilteredQuery(PagedStudentResultRequestDto input)
        {
            var Teacherdata = repository.GetAll()
                
                .Where(x => string.IsNullOrWhiteSpace(input.Keyword) ||
                            x.name.Contains(input.Keyword) ||
                            x.age.Contains(input.Keyword) 
                            
                            );
            return Teacherdata;
        }

        protected IQueryable<Teacher> ApplyPaging(IQueryable<Teacher> query, PagedStudentResultRequestDto input)
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
        protected TeacherDto MapToEntityDto(Teacher entity)
        {
            return ObjectMapper.Map<TeacherDto>(entity);
        }

        public async Task<PagedResultDto<TeacherDto>> GetAllAsync(PagedStudentResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(query);
            query = ApplyPaging(query, input);
            var classes = ClassAppService.GetAll();

            var joinQuery = from teacherdata in query
                            join classdata in classes
                            on teacherdata.ClassId equals classdata.Id into classGroup
                            from classdata in classGroup.DefaultIfEmpty() // Left Join
                            select new TeacherDto
                            {
                                Id = teacherdata.Id,
                                name = teacherdata.name,
                                age = teacherdata.age,
                                gender = teacherdata.gender,
                                maritalStatus = teacherdata.maritalStatus,
                                ClassId = teacherdata.ClassId,
                                ClassName = classdata != null ? classdata.title : "N/A" // Handle null case
                            };

            return new PagedResultDto<TeacherDto>(totalCount, joinQuery.ToList());
        }

    }
}

    //var query = CreateFilteredQuery(input);
    //var totalCount = await AsyncQueryableExecuter.CountAsync(query);
    //query = ApplySorting(query, input);
    //query = ApplyPaging(query, input);
    //// Get all classes first to avoid multiple database calls
    //var classes = ClassAppService.GetAll();
    //var joinQuery = from studentdata in query
    //                join classdata in classes
    //                on studentdata.ClassID equals classdata.Id
    //                select new StudentDto
    //                {
    //                    Id = studentdata.Id,
    //                    Name = studentdata.Name,
    //                    RollNo = studentdata.RollNo,
    //                    age = studentdata.age,
    //                    ClassId = studentdata.ClassID,
    //                    ClassName = classdata.title,
    //                };


    //return new PagedResultDto<StudentDto>(totalCount, joinQuery.ToList());


