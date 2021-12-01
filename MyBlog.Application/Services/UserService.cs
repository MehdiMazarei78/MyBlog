﻿using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entities.User;
using MyBlog.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        public bool IsExistEmail(string eamil)
        {
            return _userRepository.IsExistEmail(eamil);
        }

        public bool IsExistUserName(string userName)
        {
            return _userRepository.IsExistUserName(userName);
        }
    }
}
