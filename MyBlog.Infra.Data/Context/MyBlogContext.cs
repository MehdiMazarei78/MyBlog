using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infra.Data.Context
{
    public class MyBlogContext : DbContext
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options)
        : base(options)
        {

        }

        public DbSet<User> Users { get; set; }


    }
}
