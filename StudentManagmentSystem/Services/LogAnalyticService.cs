using StudentManagmentSystem.Data;
using StudentManagmentSystem.Infrastructure.Repositories;
using StudentManagmentSystem.Models;

namespace StudentManagmentSystem.Services
{
    public class LogAnalyticService : Repository<LogAnalytic>
    {
        public LogAnalyticService(
           SchoolContext context,
           IHttpContextAccessor httpContextAccessor,
           IAuditColumnTransformer auditColumnTransformer) :
               base(
                   context,
                   httpContextAccessor,
                   auditColumnTransformer)
        {
        }
    }
}
