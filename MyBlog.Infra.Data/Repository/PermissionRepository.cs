using MyBlog.Domain.Entities.Permissions;
using MyBlog.Domain.Entities.User;
using MyBlog.Domain.Interfaces;
using MyBlog.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infra.Data.Repository
{

    public class PermissionRepository : IPermissionRepository
    {
        private MyBlogContext _context;
        public PermissionRepository(MyBlogContext context)
        {
            _context = context;
        }

        public void AddPermissionToRole(RolePermission rolePermission)
        {
            _context.RolePermission.Add(rolePermission);
        }

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
        }

        public IEnumerable<Permission> GetPermission()
        {
            return _context.Permission;
        }

        public Role GetRoleById(int roleId)
        {
           return _context.Roles.Find(roleId);
        }

        public IEnumerable<RolePermission> GetRolePermission()
        {
            return _context.RolePermission;
        }

        public IEnumerable<Role> GetRole()
        {
            return _context.Roles;
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }

        public void RemovedPermissionInRole(RolePermission rolePermission)
        {
            _context.RolePermission.Remove(rolePermission);
        }

        public void AddRolesToUser(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
        }

        public IEnumerable<UserRole> GetUserRole()
        {
           return _context.UserRoles;
        }

        public void RemoveUserRole(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
        }
    }
}
