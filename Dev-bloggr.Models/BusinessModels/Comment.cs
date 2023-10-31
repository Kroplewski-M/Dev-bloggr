using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Dev_bloggr.Models.BusinessModels
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string UserId { get; set; }

        [ValidateNever]
        public ApplicationUser User { get; set; }

        public ICollection<BlogComments> BlogComments { get; set; }
    }
}
