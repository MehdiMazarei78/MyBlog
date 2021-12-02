﻿using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities.Post;
using MyBlog.Domain.Interfaces;
using MyBlog.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infra.Data.Repository
{
   public class BlogRepository : IBlogRepository
    {
        private MyBlogContext _context;
        public BlogRepository(MyBlogContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public IEnumerable<Post> GetAllPost()
        {
            return _context.Posts.Include(p => p.PostStatus);
        }

        public IEnumerable<PostStatus> GetStatuses()
        {
            return _context.PostStatuses;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}