using MyBlog.Application.DTOs.Users;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Security;
using MyBlog.Application.Tools;
using MyBlog.Domain.Entities.User;
using MyBlog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddUser(User user)
        {
            _userRepository.AddUser(user);
            _userRepository.Save();
            return user.UserId;
        }

        public void ChangePassword(string userName,string newPass )
        {
            var user = _userRepository.GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPass);
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }

        public bool ConfirmOldPassword(string name, string oldPass)
        {
            var hashPassword = PasswordHelper.EncodePasswordMd5(oldPass);
            return _userRepository.ConfirmOldPassword(name, hashPassword);
        }

        public void EditProfile(string username, EditProfileUserViewModel editPro)
        {
            if (editPro.UserAvatar != null)
            {
                string imagePath = "";
                if (editPro.AvatarName != "user-no-image.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar", editPro.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                editPro.AvatarName = IdGenerator.GenerateUniqCode() + Path.GetExtension(editPro.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar", editPro.AvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editPro.UserAvatar.CopyTo(stream);
                }
            }
            var user = _userRepository.GetUserByUserName(username);
            user.UserName = editPro.UserName;
            user.Email = editPro.Email;
            user.UserAvatar = editPro.AvatarName;
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }

        public EditProfileUserViewModel GetUserForEdit(string userName)
        {
            var user =  _userRepository.GetUserByUserName(userName);
            EditProfileUserViewModel editPro = new EditProfileUserViewModel()
            {
                AvatarName = user.UserAvatar ,
                Email = user.Email,
                UserName = user.UserName,
                
            };
            return editPro;
        }

        public InformationUserPanelViewModel GetUserInformationForUserPanel(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName);

            InformationUserPanelViewModel userInfo = new InformationUserPanelViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                ImageName = user.UserAvatar,
                RegisterDate = user.RegisterDate,
            };

            return userInfo;
        }

        public bool IsExistEmail(string eamil)
        {
            return _userRepository.IsExistEmail(eamil);
        }

        public bool IsExistUserName(string userName)
        {
            return _userRepository.IsExistUserName(userName);
        }

        public User LoginUser(LoginViewModel user)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(user.Password);
            string email = FixText.FixEmail(user.Email);

            return _userRepository.LoginUser(hashPassword, email);
        }
    }
}
