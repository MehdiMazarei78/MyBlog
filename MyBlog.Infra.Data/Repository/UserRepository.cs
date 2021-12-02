using MyBlog.Domain.Entities.User;
using MyBlog.Domain.Interfaces;
using MyBlog.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infra.Data.Repository
{
  public class UserRepository : IUserRepository
    {
        private MyBlogContext _context;
        public UserRepository(MyBlogContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User LoginUser(string pass , string email)
        {
            return _context.Users.SingleOrDefault(u => u.Password == pass && u.Email == email);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
