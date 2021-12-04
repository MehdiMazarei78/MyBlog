using Microsoft.EntityFrameworkCore;
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

        public bool ConfirmOldPassword(string userName, string oldPass)
        {
            return _context.Users.Any(u => u.UserName == userName && u.Password == oldPass);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName);
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

        public void UpdateUser(User user)
        {
            _context.Update(user);
        }

        public IEnumerable<User> GetUsersForEditInAdmin(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).Include(u => u.UserRoles);
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        public IEnumerable<User> GetDeleteUsers()
        {
            return _context.Users.IgnoreQueryFilters();
        }
    }
}
