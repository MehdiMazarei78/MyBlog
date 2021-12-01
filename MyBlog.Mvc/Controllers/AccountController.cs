using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.DTOs.Users;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Security;
using MyBlog.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            else if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری یا ایمیل وارد شده معتبر نمی باشد");
                return View(register);
            }
            else if (_userService.IsExistEmail(FixText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("UserName", "نام کاربری یا ایمیل وارد شده معتبر نمی باشد");
                return View(register);
            }

            User user = new User()
            {
                isDelete = false,
                RegisterDate = DateTime.Now,
                UserName = register.UserName,
                Email = FixText.FixEmail(register.Email),
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                UserAvatar = "user-no-image.jpg",
            };
            _userService.AddUser(user);

            return RedirectToAction("Login");
        }


        #endregion

    }
}
