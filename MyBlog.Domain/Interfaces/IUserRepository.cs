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
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        void AddUser(User user);
        void Save();

    }
}
