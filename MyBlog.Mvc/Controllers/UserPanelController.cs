using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.DTOs.Users;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        private IUserService _userService;

        public UserPanelController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("UserPanel")]
        public IActionResult Index()
        {
            var userInfo = _userService.GetUserInformationForUserPanel(User.Identity.Name);
            return View(userInfo);
        }

        #region EditProfile

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            var user = _userService.GetUserForEdit(User.Identity.Name);

            return View(user);
        }
        [HttpPost]
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile(EditProfileUserViewModel editPro)
        {
            if (!ModelState.IsValid)
            {
                return View(editPro);
            }
            else if (_userService.IsExistUserName(editPro.UserName) && User.Identity.Name != editPro.UserName)
            {
                ModelState.AddModelError("UserName", "نام کاربری یا ایمیل وارد شده معتبر نمی باشد");
                return View(editPro);
            }
            else if (_userService.IsExistEmail(FixText.FixEmail(editPro.Email)) && User.FindFirstValue(ClaimTypes.Email) != editPro.Email)
            {
                ModelState.AddModelError("UserName", "نام کاربری یا ایمیل وارد شده معتبر نمی باشد");
                return View(editPro);
            }

            _userService.EditProfile(User.Identity.Name, editPro);
            ViewBag.isSuccess = true;

            return Redirect("/Logout");
        }

        #endregion

        #region Change Password
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel changePass)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (!_userService.ConfirmOldPassword(User.Identity.Name,changePass.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمی باشد.");
                return View(changePass);
            }

            _userService.ChangePassword(User.Identity.Name, changePass.Password);
            ViewBag.isSuccess = true;
            return View();
        }

        #endregion
    }
}
