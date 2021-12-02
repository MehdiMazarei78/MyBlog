using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Application.DTOs.Posts;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entities.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {

        private IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
       
        public IActionResult Index()
        {
            var blogs = _blogService.GetAllUserPost(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            
            return View(blogs);
        }


        #region AddPost

        [Route("AddPost")]
        public IActionResult AddPost()
        {
            var statuses = _blogService.GetStatuses();
            ViewData["Statues"] = new SelectList(statuses, "Value", "Text");
            return View();
        }
        [HttpPost]
        [Route("AddPost")]
        public IActionResult AddPost(Post post, IFormFile imgPostUp)
        {
            if (!ModelState.IsValid)
                return View(post);

            _blogService.AddBlog(post, imgPostUp);

            return Redirect("Blog");
        }

        #endregion

    }
}

