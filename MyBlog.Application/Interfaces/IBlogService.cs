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
        List<ListPostForUserViewModel> GetAllUserPost(int userId);
        Post GetPostByPostId(int postId);
        List<ListPostForUserViewModel> GetDeletePost(int userId);
        PostForDeleteViewModel GetPostForDelete(int postId);

        int AddBlog(Post post, IFormFile imgPost);
        void EditPost(Post post, IFormFile imgPost);
        void DeletePost(int postId);
    }
}
