using MyBlog.Domain.Entities.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Interfaces
{
   public interface IBlogRepository
    {
        IEnumerable<PostStatus> GetStatuses();
        IEnumerable<Post> GetAllPost();
        void AddPost(Post post);
        void Save();
    }
}
