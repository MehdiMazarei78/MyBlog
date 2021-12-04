using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entities.Permissions;
using MyBlog.Domain.Entities.User;
using MyBlog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public void AddPermissionToRole(int roleId, List<int> selectedPermission)
        {
            foreach (var p in selectedPermission)
            {
                _permissionRepository.AddPermissionToRole(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId,
                });
                _permissionRepository.save();
            }
        }

        public int AddRole(Role role)
        {
            _permissionRepository.AddRole(role);
            _permissionRepository.save();
            return role.RoleId;
        }

        public List<Permission> GetPermissions()
        {
            return _permissionRepository.GetPermissions().ToList();
        }

        public List<Role> GetRoles()
        {
            return _permissionRepository.GetRoles().ToList();
        }
    }
}
