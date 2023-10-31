using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev_bloggr.Models.BusinessModels
{
    public class BlogComments
    {
        [Key]   
        public int Id { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }

    }
}
