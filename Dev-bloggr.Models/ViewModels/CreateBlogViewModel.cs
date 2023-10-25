using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev_bloggr.Models.ViewModels
{
    public class CreateBlogViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Header { get; set; }

        [Required]
        [MinLength(200)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation property for the user
        [ValidateNever]
        public ApplicationUser User { get; set; }
        //Foreign key for the user
        public string UserId { get; set; }

    }
}
