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
        List<ListPostForAdminViewModel> GetAllPostsForAdmin();
        Post GetPostByPostId(int postId);
        List<ListPostForUserViewModel> GetDeletePost(int userId);
        PostForDeleteViewModel GetPostForDelete(int postId);
        Tuple<List<ListPostsForShowInIndexViewModel>, int> GetAllPostsForShow(int pageId = 1, int take = 0);

        int AddBlog(Post post, IFormFile imgPost);
        void EditPost(Post post, IFormFile imgPost);
        void DeletePost(int postId);

        DeletePostForAdminViewModel GetPostForDeleteForAdmin(int postId);
    }
}
