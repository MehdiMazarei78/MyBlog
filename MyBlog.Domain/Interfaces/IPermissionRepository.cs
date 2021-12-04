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
        IEnumerable<Role> GetRole();
        IEnumerable<Permission> GetPermission();
        IEnumerable<RolePermission> GetRolePermission();

        Role GetRoleById(int roleId);

        void AddRole(Role role);
        void UpdateRole(Role role);
        
        void AddPermissionToRole(RolePermission rolePermission);
        void RemovedPermissionInRole(RolePermission rolePermission);
        void save();
    }
}
