using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Application.DTOs.Posts;
using MyBlog.Application.Interfaces;

namespace MyBlog.Mvc.Pages.Admin.Posts
{
    public class DeletePostModel : PageModel
    {
        private IBlogService _blogService;
        public DeletePostModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [BindProperty]
        public DeletePostForAdminViewModel PostInfo { get; set; }
        public void OnGet(int id)
        {
            PostInfo = _blogService.GetPostForDeleteForAdmin(id);
        }

        public IActionResult OnPost()
        {
            _blogService.DeletePost(PostInfo.PostId);
            return RedirectToPage("Index");
        }
    }
}
