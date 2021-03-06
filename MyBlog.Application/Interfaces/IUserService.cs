using MyBlog.Application.DTOs.Users;
using MyBlog.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces
{
   public  interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string eamil);
        bool ConfirmOldPassword(string name ,string oldPass);
        int AddUser(User user);
        int AddUserFromAdmin(CreateUserForAdminViewModel user);

        User LoginUser(LoginViewModel user);

        EditProfileUserViewModel GetUserForEdit(string userName);
        void EditProfile(string username, EditProfileUserViewModel editPro);
        void ChangePassword(string userName,string newPass);

        InformationUserPanelViewModel GetUserInformationForUserPanel(string userName);


        UsersForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        EditUserForAdminViewModel GetUserForShowInEditMode(int userId);
        void EditUserForAdmin(EditUserForAdminViewModel editUser);
        InformationUserForAdminViewModel GetUserInformation(int userId);
        void DeleteUser(int userId);
        UsersForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

    }
}
