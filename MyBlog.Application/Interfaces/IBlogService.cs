using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Application.DTOs.Posts;
using MyBlog.Domain.Entities.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces
{
    public interface IBlogService
    {
        List<SelectListItem> GetStatuses();
        List<ListPostForUser> GetAllUserPost(int userId);
        int AddBlog(Post post, IFormFile imgPost);
    }
}
