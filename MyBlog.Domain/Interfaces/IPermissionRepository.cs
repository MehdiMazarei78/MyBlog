using MyBlog.Domain.Entities.Permissions;
using MyBlog.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Interfaces
{
    public interface IPermissionRepository
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<Permission> GetPermissions();

        void AddRole(Role role);
        void AddPermissionToRole(RolePermission rolePermission);
        void save();
    }
}
