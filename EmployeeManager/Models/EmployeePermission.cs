using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager
{
    public class EmployeePermission
    {
        [Key, Column(Order = 0)]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
        [Key, Column(Order = 1)]
        public int PermissionId { get; set; }
        public Permission Category { get; set; }
    }
}