using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.EntityFrameworkCore
{
    public static class SchoolManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SchoolManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<SchoolManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
