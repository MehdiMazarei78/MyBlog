using MyBlog.Domain.Entities.Permissions;
using MyBlog.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces
{
   public interface IPermissionService
    {
        List<Role> GetRoles();
        List<Permission> GetPermissions();

        int AddRole(Role role);

        void AddPermissionToRole(int roleId, List<int> selectedPermission);
    }
}
