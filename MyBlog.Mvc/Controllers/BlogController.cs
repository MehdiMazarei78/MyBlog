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
            {
                var statuses = _blogService.GetStatuses();
                ViewData["Statues"] = new SelectList(statuses, "Value", "Text");
                return View(post);
            }    
            
            

            _blogService.AddBlog(post, imgPostUp);

            return Redirect("Blog");
        }

        #endregion

        #region EditPost

        [Route("EditPost/{id}")]
        public IActionResult EditPost(int id)
        {
            var post = _blogService.GetPostByPostId(id);

            var statues = _blogService.GetStatuses();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");

            return View(post);
        }
        [HttpPost]
        [Route("EditPost/{id}")]
        public IActionResult EditPost(Post post , IFormFile imgPostUp)
        {
            if (!ModelState.IsValid)
            {
                var statues = _blogService.GetStatuses();
                ViewData["Statues"] = new SelectList(statues, "Value", "Text");
                return View(post);
            }
            _blogService.EditPost(post, imgPostUp);
           

            return Redirect("/Blog");
        }

        #endregion

        #region DeletePost

        [Route("DeletePost/{id}")]
        public IActionResult DeletePost(int id)
        {
            ViewData["PostId"] = id;
            var post = _blogService.GetPostForDelete(id);
            return View(post);
        }

        [HttpPost]
        [Route("DeletePost/{id}")]
        public IActionResult DeletePost(string id)
        {
            _blogService.DeletePost(int.Parse(id));

            return Redirect("/Blog");
        }

        #endregion

        #region ListDeletePost

        [Route("ListDeletePost")]
        public IActionResult ListDeletePost()
        {
            var deletePost = _blogService.GetDeletePost(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View(deletePost);
        }

        #endregion
    }
}

