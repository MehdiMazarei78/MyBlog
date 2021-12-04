using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities.Permissions;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        public DbSet<Post> Posts { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Post>()
                 .HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<User>()
               .HasQueryFilter(u => !u.isDelete);
            modelBuilder.Entity<Role>()
                 .HasQueryFilter(r => !r.IsDelete);



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
