using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolManagement.Authorization.Users;
using SchoolManagement.Classes;
using SchoolManagement.Classes.Dto;
using SchoolManagement.Exams.Dto;
using SchoolManagement.Students;
using SchoolManagement.Students.Dto;
using SchoolManagement.Teachers;
using SchoolManagement.Teachers.Dto;
using SchoolManagement.Users.Dto;

namespace SchoolManagement
{
    public class CustomDtoMapper
    {
        public static void ConfigureBasicInfoMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Student,StudentDto>().ReverseMap();
            configuration.CreateMap<Student, CreateStudentDto>().ReverseMap();
            configuration.CreateMap<StudentClass,ClassDto>().ReverseMap();
            configuration.CreateMap<StudentClass, CreateClassDto>().ReverseMap();
            configuration.CreateMap<Teacher,TeacherDto>().ReverseMap();
            configuration.CreateMap<Teacher, CreateTeacherDto>().ReverseMap();
            configuration.CreateMap<Exam.Exam,CreateExamDto>().ReverseMap();
            configuration.CreateMap<Exam.Exam, ExamDto>().ReverseMap();
            configuration.CreateMap<UserEditDto, User>().ReverseMap();



        }
    }
}
