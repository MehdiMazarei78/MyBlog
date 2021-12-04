using MyBlog.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetDeleteUsers();
        IEnumerable<User> GetUsersForEditInAdmin(int userId);

        User GetUserByUserName(string userName);
        User GetUserByUserId(int userId);

        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool ConfirmOldPassword(string userName, string oldPass);
        void AddUser(User user);
        void UpdateUser(User user);
        User LoginUser(string pass , string email);

        void Save();

    }
}
