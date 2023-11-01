using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev_bloggr.Models.BusinessModels;

namespace Dev_bloggr.Models.ViewModels
{
    public class BlogViewModel
    {
        public Blog blog { set; get; }
        public AddCommentViewModel addComment { set; get; }
    }
}
