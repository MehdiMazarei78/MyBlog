using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.DTOs.Posts
{
    public class ListPostForUserViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ImageName { get; set; }
    }
    public class ListPostForAdminViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public string ImageName { get; set; }
    }
    public class DeletePostForAdminViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class PostForDeleteViewModel
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class ListPostsForShowInIndexViewModel
    {
        public int PostId { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
