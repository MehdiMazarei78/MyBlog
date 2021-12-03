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

        public void DeletePost(int postId)
        {
            var post = _blogRepository.GetPostByPostId(postId);

            post.IsDelete = true;

            _blogRepository.UpdatePost(post);
            _blogRepository.Save();
        }

        public void EditPost(Post post, IFormFile imgPost)
        {
            if (imgPost != null && imgPost.IsImage())
            {
                if (post.PostImageName != "no-image.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/PostImage/image", post.PostImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/PostImage/thumb", post.PostImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                post.PostImageName= IdGenerator.GenerateUniqCode() + Path.GetExtension(imgPost.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/PostImage/image", post.PostImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgPost.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/PostImage/thumb", post.PostImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }
            _blogRepository.UpdatePost(post);
            _blogRepository.Save();
        }

        public List<ListPostForUserViewModel> GetAllUserPost(int userId)
        {
            return _blogRepository.GetAllPost().Where(p => p.UserId == userId).Select(p => new ListPostForUserViewModel()
            {
                ImageName = p.PostImageName,
                PostId = p.PostId,
                Status = p.PostStatus.StatusTitle,
                Title = p.PostTitle
            }).ToList();
        }

        public List<ListPostForUserViewModel> GetDeletePost(int userId)
        {
          return _blogRepository.GetAllDeletePost().Where(p => p.IsDelete == true && p.UserId == userId).Select(p => new ListPostForUserViewModel()
            {
                ImageName = p.PostImageName,
                PostId = p.PostId,
                Status = p.PostStatus.StatusTitle,
                Title = p.PostTitle
            }).ToList();
        }

        public Post GetPostByPostId(int postId)
        {
            return _blogRepository.GetPostByPostId(postId);
        }

        public PostForDeleteViewModel GetPostForDelete(int postId)
        {
            var post = _blogRepository.GetPostForDelete(postId);

            return new PostForDeleteViewModel()
            {
                CreateDate = post.CreateDate,
                Status = post.PostStatus.StatusTitle,
                Title = post.PostTitle
            };
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
