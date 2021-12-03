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
        IEnumerable<Post> GetAllDeletePost();
        void AddPost(Post post);
        void UpdatePost(Post post);

        Post GetPostByPostId(int postId);
        Post GetPostForDelete(int postId);
        void Save();
    }
}
