using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Application.DTOs.Users;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Security;

namespace MyBlog.Mvc.Pages.Admin.ManageUser
{
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;

        }

        [BindProperty]
        public EditUserForAdminViewModel EditUserForAdminViewModel { get; set; }
        public void OnGet(int id)
        {
            EditUserForAdminViewModel = _userService.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            _userService.EditUserForAdmin(EditUserForAdminViewModel);

            _permissionService.EditRolesUserForAdmin(EditUserForAdminViewModel.UserId, selectedRoles);

            return RedirectToPage("Index");
        }
    }
}
