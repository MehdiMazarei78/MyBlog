using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Application.DTOs.Posts;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entities.Post;

namespace MyBlog.Mvc.Pages.Admin.Posts
{
    public class IndexModel : PageModel
    {
        private IBlogService _blogService;

        public IndexModel(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public List<ListPostForAdminViewModel> ListPost { get; set; }
        public void OnGet()
        {

            ListPost = _blogService.GetAllPostsForAdmin();
        }
    }
}

