using StudentManagmentSystem.Models.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagmentSystem.Models
{
    [Table("LogAnalytics")]
    public class LogAnalytic : _Base
    {
        public LogAnalytic() { }
        public string? UserName { get; set; }
        public string? IPAddress { get; set; }
        public string? Url { get; set; }
        public string? Device { get; set; }
        public string? GeographicLocation { get; set; }
        public string? Browser { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CompanyId { get; set; }
   
    }
}
