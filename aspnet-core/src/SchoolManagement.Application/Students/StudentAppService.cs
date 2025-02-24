using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Authorization;
using SchoolManagement.Authorization.Users;
using SchoolManagement.Classes;
using SchoolManagement.Classes.Dto;
using SchoolManagement.Students.Dto;
using SchoolManagement.Teachers;
using SchoolManagement.Teachers.Dto;
using SchoolManagement.Users.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchoolManagement.Students
{
    //[AbpAuthorize(PermissionNames.pages_School_Management_project)]
    //[AbpAuthorize(PermissionNames.pages_StudentManagement_permission)]
    public class StudentAppService: AsyncCrudAppService<Student, StudentDto, int, PagedStudentResultRequestDto, CreateStudentDto, StudentDto>, IStudentAppService
    {
        public readonly IRepository<Student,int> _repository;
        public readonly IRepository<StudentClass> ClassAppService;
        public readonly IRepository<Teacher> TeacherAppService;

        public StudentAppService(IRepository<Student, int> repository,
                                IRepository<StudentClass> _ClassAppService,
                                IRepository<Teacher> _TeacherAppService)
        : base(repository)
        {
            _repository = repository;
            ClassAppService = _ClassAppService;
            TeacherAppService = _TeacherAppService;
        }

        protected IQueryable<StudentDto> CreateFilteredQuery(PagedStudentResultRequestDto input, IQueryable<StudentDto> joinQuery)
        {
            return  joinQuery
                    .Where(x => string.IsNullOrWhiteSpace(input.Keyword) ||
                            x.Name.Contains(input.Keyword) ||
                            x.RollNo.Contains(input.Keyword) ||
                            x.ClassName.Contains(input.Keyword) ||
                            x.TeacherName.Contains(input.Keyword) ||
                            x.age.Contains(input.Keyword));
        }
        public override async Task<PagedResultDto<StudentDto>> GetAllAsync(PagedStudentResultRequestDto input)
        {
            var Students = Repository.GetAll();
            var classes = ClassAppService.GetAll();
            var Teachers = TeacherAppService.GetAll();
            var totalCount = await AsyncQueryableExecuter.CountAsync(Students);
            var joinQuery = from student in Students
                            join classData in classes
                            on student.ClassID equals classData.Id into classGroup

                            from classData in classGroup.DefaultIfEmpty() // LEFT JOIN on Classgroup only give that class which assigined to student
                            join teacherData in Teachers
                            on classData.Id equals teacherData.ClassId into teacherGroup

                            from teacherData in teacherGroup.DefaultIfEmpty() // LEFT JOIN on Teacher
                            select new StudentDto
                            {
                                Id = student.Id,
                                Name = student.Name,
                                RollNo = student.RollNo,
                                age = student.age,
                                gender = student.gender,
                                maritalStatus = student.maritalStatus,
                                ClassId = student.ClassID,
                                ClassName = classData != null ? classData.title : "N/A", // Handle null case
                                TeacherName = teacherData != null ? teacherData.name : "N/A" // Handle null case
                            };
            var query = CreateFilteredQuery(input,joinQuery);
                query = ApplyPaging(query, input);
            return new PagedResultDto<StudentDto>(totalCount, query.ToList());
        }
        protected  IQueryable<StudentDto> ApplyPaging(IQueryable<StudentDto> query, PagedStudentResultRequestDto input)
        {
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }
            //var limitedInput = input as ILimitedResultRequest;
            //if (limitedInput != null)
            //{
            //    return query.Take(limitedInput.MaxResultCount);
            //}
            return query;
        }
    }
}





//Get all classes first to avoid multiple database calls

//var joinQuery = from student in query
//                join classData in ClassAppService.GetAll()
//                on student.ClassID equals classData.Id 

//                join teacherData in TeacherAppService.GetAll()
//                on classData.Id equals teacherData.ClassId

//                select new StudentDto
//                {
//                    Id = student.Id,
//                    Name = student.Name,
//                    RollNo = student.RollNo,
//                    age = student.age,
//                    gender = student.gender,
//                    maritalStatus = student.maritalStatus,
//                    ClassId = student.ClassID,
//                    ClassName = classData != null ? classData.title : "N/A", // Handle null case
//                    TeacherName = teacherData != null ? teacherData.name : "N/A" // Handle null case
//                };