using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities.Post;
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
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Post>()
                 .HasQueryFilter(p => !p.IsDelete);


            modelBuilder.Entity<Post>()
          .HasOne<PostStatus>(p => p.PostStatus)
          .WithMany(p => p.Posts)
          .HasForeignKey(p => p.StatusId);
            
            modelBuilder.Entity<Post>()
          .HasOne<User>(p => p.User)
          .WithMany(p => p.Posts)
          .HasForeignKey(p => p.UserId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
