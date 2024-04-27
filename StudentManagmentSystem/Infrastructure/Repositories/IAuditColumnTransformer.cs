using StudentManagmentSystem.Data;
using StudentManagmentSystem.Models.Contracts;

namespace StudentManagmentSystem.Infrastructure.Repositories
{
    public interface IAuditColumnTransformer
    {
        Task TransformAsync(IHasAudit entity, SchoolContext context);
    }
}
