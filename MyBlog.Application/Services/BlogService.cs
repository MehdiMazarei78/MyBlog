using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entities.Post;
using MyBlog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Application.Security;
using MyBlog.Application.Tools;
using System.IO;
using MyBlog.Application.DTOs.Posts;

namespace MyBlog.Application.Services
{
   public class BlogService : IBlogService
    {
        private IBlogRepository _blogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public int AddBlog(Post post, IFormFile imgPost)
        {
            post.CreateDate = DateTime.Now;
            post.PostImageName = "no-image.png";

            if (imgPost != null && imgPost.IsImage())
            {
                post.PostImageName = IdGenerator.GenerateUniqCode() + Path.GetExtension(imgPost.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/PostImage/image", post.PostImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgPost.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/PostImage/thumb", post.PostImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }
            _blogRepository.AddPost(post);
            _blogRepository.Save();
            return post.PostId;
        }

        public List<ListPostForUser> GetAllUserPost(int userId)
        {
            return _blogRepository.GetAllPost().Where(b => b.UserId == userId).Select(b => new ListPostForUser()
            {
                ImageName = b.PostImageName,
                PostId = b.PostId,
                Status = b.PostStatus.StatusTitle,
                Title = b.PostTitle
            }).ToList();
        }

        public List<SelectListItem> GetStatuses()
        {
            return _blogRepository.GetStatuses().Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle
            }).ToList();
        }
    }
}
