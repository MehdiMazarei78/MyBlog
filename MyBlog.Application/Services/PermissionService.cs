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

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            _permissionRepository.UpdateRole(role);
            _permissionRepository.save();
        }

        public List<Permission> GetPermissions()
        {
            return _permissionRepository.GetPermission().ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _permissionRepository.GetRoleById(roleId);
        }

        public List<int> GetRolePermission(int roleId)
        {
            return _permissionRepository.GetRolePermission().Where(r => r.RoleId == roleId).Select(r => r.PermissionId).ToList();
        }

        public List<Role> GetRoles()
        {
            return _permissionRepository.GetRole().ToList();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _permissionRepository.GetRolePermission()
                .Where(r => r.RoleId == roleId)
                .ToList()
                .ForEach(r =>
                _permissionRepository.RemovedPermissionInRole(r)
                );
            AddPermissionToRole(roleId, permissions);
            _permissionRepository.save();
        }

        public void UpdateRole(Role role)
        {
            _permissionRepository.UpdateRole(role);
            _permissionRepository.save();
        }
    }
}
