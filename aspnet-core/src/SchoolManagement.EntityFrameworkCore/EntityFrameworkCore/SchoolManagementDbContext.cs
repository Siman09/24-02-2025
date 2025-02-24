using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using SchoolManagement.Authorization.Roles;
using SchoolManagement.Authorization.Users;
using SchoolManagement.MultiTenancy;
using JetBrains.Annotations;
using SchoolManagement.Students;
using SchoolManagement.Classes;
using SchoolManagement.Teachers;
using SchoolManagement.Exam;

namespace SchoolManagement.EntityFrameworkCore
{
    public class SchoolManagementDbContext : AbpZeroDbContext<Tenant, Role, User, SchoolManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
            : base(options)
        { }
            public DbSet<Student> students { get; set; }
            public DbSet<StudentClass> classes { get; set; }
            public DbSet<Teacher> teachers { get; set; }
            public DbSet<Exam.Exam> exams { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            //base.OnModelCreating(modelBuilder);

            //// Allow nullable foreign key for Teacher
            //modelBuilder.Entity<Teacher>()
            //    .HasOne(e => e.classes)
            //    .WithMany()
            //    .HasForeignKey(e => e.ClassId)
            //    .OnDelete(DeleteBehavior.Cascade); // ✅ Set NULL when Class is deleted

            // Allow nullable foreign key for Student
            //modelBuilder.Entity<Student>()
            //    .HasOne(e => e.Class)
            //    .WithMany()
            //    .HasForeignKey(e => e.ClassID)
            //    .OnDelete(DeleteBehavior.SetNull); // ✅ Set NULL when Class is deleted

        //}

    }
}
