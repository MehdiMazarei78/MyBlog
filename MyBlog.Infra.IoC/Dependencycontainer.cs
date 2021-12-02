using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Services;
using MyBlog.Domain.Interfaces;
using MyBlog.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infra.IoC
{
   public class Dependencycontainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            //Application Layer
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IBlogService, BlogService>();

            //Infra Data Layer
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IBlogRepository, BlogRepository>();
        }
    }
}
