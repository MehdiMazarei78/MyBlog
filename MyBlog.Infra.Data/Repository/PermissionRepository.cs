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

        public IEnumerable<Permission> GetPermissions()
        {
            return _context.Permission;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles;
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
