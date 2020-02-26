using System.Collections.Generic;

namespace EmployeeManager
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }

        public ICollection<EmployeePermission> EmployeePermissions { get; set; }
    }
}