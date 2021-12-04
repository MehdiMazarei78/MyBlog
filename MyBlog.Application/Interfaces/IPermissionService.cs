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
        bool CheckPermission(int permissionId, string userName);

        List<Role> GetRoles();
        List<Permission> GetPermissions();

        Role GetRoleById(int roleId);
        List<int> GetRolePermission(int roleId);

        int AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void UpdatePermissionsRole(int roleId, List<int> permissions);

        void AddPermissionToRole(int roleId, List<int> selectedPermission);
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditRolesUserForAdmin(int userId, List<int> roleIds);
    }
}
