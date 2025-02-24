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
using Abp.UI;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Authorization;
using SchoolManagement.Authorization.Roles;
using SchoolManagement.Classes.Dto;
using SchoolManagement.Roles.Dto;
using SchoolManagement.Students;
using SchoolManagement.Students.Dto;

namespace SchoolManagement.Classes
{
    [AbpAuthorize(PermissionNames.pages_School_Management_project)]
    [AbpAuthorize(PermissionNames.pages_ClassManagement_permission)]
    public class ClassAppService : SchoolManagementAppServiceBase,IClassAppService
    {
        public readonly IRepository<StudentClass> studentClassAppservice;
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }

        public ClassAppService(IRepository<StudentClass> _studentAppservice)
        {
            studentClassAppservice = _studentAppservice;
        }

        [AbpAuthorize(PermissionNames.pages_CreateClass_permission)]
        public async Task CreateAsync(CreateClassDto input)
        {
            await studentClassAppservice.InsertAsync(ObjectMapper.Map<StudentClass>(input));
            
        }

        [AbpAuthorize(PermissionNames.pages_DeleteClass_permission)]
        public async Task DeleteAsync(EntityDto<int> input)
        {
            try
            {
                var std = await studentClassAppservice.FirstOrDefaultAsync(s => s.Id == input.Id);
                await studentClassAppservice.DeleteAsync(std);
            }catch(Exception ex)
            {
                throw new UserFriendlyException("error:-"+ex);
            }
        }

        public async Task<ClassDto> GetAsync(int input)
        {
            var data=await studentClassAppservice.FirstOrDefaultAsync(x=>x.Id==input);
            return MapToEntityDto(data);
        }

        [AbpAuthorize(PermissionNames.pages_EditClass_permission)]
        public async Task UpdateAsync(ClassDto input)
        {
            await studentClassAppservice.UpdateAsync(ObjectMapper.Map<StudentClass>(input));
            
        }

        public async Task<PagedResultDto<ClassDto>> GetAllAsync(PagedStudentResultRequestDto input)
           {
            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            //query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<ClassDto>( totalCount,entities.Select(MapToEntityDto).ToList());
        }

        protected IQueryable<StudentClass> CreateFilteredQuery(PagedStudentResultRequestDto input)
        {
            return studentClassAppservice.GetAll()
                .Where(x => string.IsNullOrWhiteSpace(input.Keyword) || 
                                   x.title.Contains(input.Keyword));
        }
        protected  IQueryable<StudentClass> ApplyPaging(IQueryable<StudentClass> query, PagedStudentResultRequestDto input)
        {
            //Try to use paging if available
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
               var data=query.PageBy(pagedInput);
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
        protected  ClassDto MapToEntityDto(StudentClass entity)
        {
            return ObjectMapper.Map<ClassDto>(entity);
        }
    }
}
