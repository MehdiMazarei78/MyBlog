using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private IBlogService _blogService;
        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IActionResult Index(int pageId = 1)
        {
            var posts = _blogService.GetAllPostsForShow(pageId, 9);

            ViewBag.pageId = pageId;

            return View(posts);
        }

        [Route("SinglePost/{id}")]
        public IActionResult SinglePost(int id)
        {
            var post = _blogService.GetPostByPostId(id);

            return View(post);
        }
    }
}
