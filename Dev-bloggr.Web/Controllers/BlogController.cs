using Dev_bloggr.DataAccess.Data;
using Dev_bloggr.Models.BusinessModels;
using Dev_bloggr.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dev_bloggr.Web.Controllers
{
    public class BlogController: Controller
    {
        private readonly ApplicationDbContext _db;
        public BlogController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult CreateBlog()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateBlog(CreateBlogViewModel blog)
        {
            if (String.IsNullOrEmpty(blog.Content))
            {
                return View();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    var blogInfo = new Blog
                    {
                        Title = blog.Title,
                        Header = blog.Header,
                        Content = blog.Content,
                        CreatedAt = DateTime.Now,
                        UserId = userId // Set the UserId property to associate the Blog with the user.
                    };
                    _db.Blogs.Add(blogInfo);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public IActionResult YourBlogs()
        {
            return View();
        }
    }
}
