using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev_bloggr.Models.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public int BlogId { get; set; }
        [Required]

        public string UserId { get; set; }
        [Required]
        [Display(Name = "Add Comment")]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}
