using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entities.Post;

namespace MyBlog.Mvc.Pages.Admin.Posts
{
    public class EditPostModel : PageModel
    {
        private IBlogService _blogService;

        public EditPostModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [BindProperty]
        public Post Post { get; set; }
        public void OnGet(int id)
        {
            Post = _blogService.GetPostByPostId(id);

            var statues = _blogService.GetStatuses();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");
        }
        public IActionResult OnPost(IFormFile imgBlogUp)
        {
            if (!ModelState.IsValid)
            {
                var statues = _blogService.GetStatuses();
                ViewData["Statues"] = new SelectList(statues, "Value", "Text");
                return Page();
            }
                

            _blogService.EditPost(Post, imgBlogUp);
            return RedirectToPage("Index");
        }
    }
}
