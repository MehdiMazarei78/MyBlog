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
    public class CreatePostModel : PageModel
    {
        private IBlogService _blogService;
        public CreatePostModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [BindProperty]
        public Post Post { get; set; }
        public void OnGet()
        {

            var statues = _blogService.GetStatuses();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");

        }
        public IActionResult OnPost(IFormFile imgPostUp)
        {
            if (!ModelState.IsValid)
            {
                var statues = _blogService.GetStatuses();
                ViewData["Statues"] = new SelectList(statues, "Value", "Text");
                return Page();
            }
               

            _blogService.AddBlog(Post, imgPostUp);

            return RedirectToPage("Index");
        }
    }
}
