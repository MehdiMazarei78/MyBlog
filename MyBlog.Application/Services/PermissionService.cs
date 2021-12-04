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
        private IUserRepository _userRepository;
        public PermissionService(IPermissionRepository permissionRepository , IUserRepository userRepository)
        {
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
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

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (var roleId in roleIds)
            {
                _permissionRepository.AddRolesToUser(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId,
                });
                _permissionRepository.save();
            }
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _userRepository.GetUserByUserName(userName).UserId;

            List<int> UserRoles = _permissionRepository.GetUserRole()
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleId).ToList();

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _permissionRepository.GetRolePermission()
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            _permissionRepository.UpdateRole(role);
            _permissionRepository.save();
        }

        public void EditRolesUserForAdmin(int userId, List<int> roleIds)
        {
            _permissionRepository.GetUserRole()
                .Where(u => u.UserId == userId)
                .ToList()
                .ForEach(u =>
                _permissionRepository.RemoveUserRole(u)
            );
            AddRolesToUser(roleIds, userId);
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
