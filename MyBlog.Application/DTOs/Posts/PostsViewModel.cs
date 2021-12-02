using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.DTOs.Posts
{
    public class ListPostForUser
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ImageName { get; set; }
    }

}
