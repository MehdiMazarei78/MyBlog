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
        int AddUser(User user);

        User LoginUser(LoginViewModel user);

        EditProfileUserViewModel GetUserForEdit(string userName);
        void EditProfile(string username, EditProfileUserViewModel editPro);

        InformationUserPanelViewModel GetUserInformationForUserPanel(string userName);

    }
}
