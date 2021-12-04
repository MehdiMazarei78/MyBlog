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
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public CreateUserModel(IUserService userService , IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;

        }

        [BindProperty]
        public CreateUserForAdminViewModel createUserForAdminViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();


            var userId = _userService.AddUserFromAdmin(createUserForAdminViewModel);


            _permissionService.AddRolesToUser(SelectedRoles, userId);

            return Redirect("/Admin/ManageUser");
        }
    }
}
