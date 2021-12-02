using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Entities.Post
{
   public  class Post
    {
        [Key]
        public int PostId { get; set; }

        public int StatusId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "عنوان پست")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PostTitle { get; set; }

        [Display(Name = "شرح مختصر پست")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PostSmalDescription { get; set; }

        [Display(Name = "شرح پست")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PostDescription { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string PostImageName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public bool IsDelete { get; set; }

        #region Relations

        public PostStatus PostStatus { get; set; }
        public MyBlog.Domain.Entities.User.User User { get; set; }

        #endregion
    }
}
