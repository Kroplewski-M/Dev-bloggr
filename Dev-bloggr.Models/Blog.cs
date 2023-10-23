using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev_bloggr.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property for the user
        public ApplicationUser User { get; set; }
        //Foreign key for the user
        public string UserId { get; set; }

    }
}
