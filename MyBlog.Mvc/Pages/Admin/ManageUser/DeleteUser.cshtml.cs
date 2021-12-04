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
    [PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public InformationUserForAdminViewModel InformationUserViewModel { get; set; }
        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = _userService.GetUserInformation(id);
        }
        public IActionResult OnPost(int userId)
        {

            _userService.DeleteUser(userId);

            return RedirectToPage("Index");
        }
    }
}
