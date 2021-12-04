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

        public int AddUserFromAdmin(CreateUserForAdminViewModel user)
        {
            User addUser = new User();
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.Email = user.Email;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;

            if (user.UserAvatar != null)
            {
                string imagePath = "";

                addUser.UserAvatar = IdGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar", addUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }

            _userRepository.AddUser(addUser);
            _userRepository.Save();
            return addUser.UserId;
        }

        public void ChangePassword(string userName, string newPass)
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

        public void DeleteUser(int userId)
        {
            var user = _userRepository.GetUserByUserId(userId);
            user.isDelete = true;
            _userRepository.Save();
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

        public void EditUserForAdmin(EditUserForAdminViewModel editUser)
        {
            User user = _userRepository.GetUserByUserId(editUser.UserId);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }
            if (editUser.UserAvatar != null)
            {

                //Delete Old Image
                if (editUser.AvatarName != "user-no-image.jpg")
                {

                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                string imagePath = "";


                user.UserAvatar = IdGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }

        public UsersForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            var res = _userRepository.GetDeleteUsers().Where(u => u.isDelete == true);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                res = res.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                res = res.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = res.Count() / take;
            list.Users = res.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public EditProfileUserViewModel GetUserForEdit(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            EditProfileUserViewModel editPro = new EditProfileUserViewModel()
            {
                AvatarName = user.UserAvatar,
                Email = user.Email,
                UserName = user.UserName,

            };
            return editPro;
        }

        public EditUserForAdminViewModel GetUserForShowInEditMode(int userId)
        {
            return _userRepository.GetUsersForEditInAdmin(userId).Select(u => new EditUserForAdminViewModel()
            {
                UserId = u.UserId,
                UserName = u.UserName,
                AvatarName = u.UserAvatar,
                UserRoles = u.UserRoles.Select(r => r.RoleId).ToList(),
                Email = u.Email,
            }).Single();

        }

        public InformationUserForAdminViewModel GetUserInformation(int userId)
        {
            var user = _userRepository.GetUserByUserId(userId);
            InformationUserForAdminViewModel infoUser = new InformationUserForAdminViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
            };
            return infoUser;
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

        public UsersForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            var res = _userRepository.GetUsers();

            if (!string.IsNullOrEmpty(filterEmail))
            {
                res = res.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                res = res.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UsersForAdminViewModel list = new UsersForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = res.Count() / take;
            list.Users = res.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;

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
