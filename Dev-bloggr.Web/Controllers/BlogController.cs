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
        private readonly ILogger<BlogController> _logger;
        public BlogController(ApplicationDbContext db, ILogger<BlogController> logger)
        {
            _db = db;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult UpsertBlog(int? id)
        {
            var blogViewModel = new CreateBlogViewModel();
            
            if (id == null || id == 0)
            {
                return View(blogViewModel);
            }
            else
            {
                var blog = _db.Blogs.FirstOrDefault(u => u.Id == id);
                blogViewModel.Id = blog.Id;
                blogViewModel.Title = blog.Title;
                blogViewModel.Header = blog.Header;
                blogViewModel.Content = blog.Content;
                blogViewModel.CreatedAt = blog.CreatedAt;
                blogViewModel.UserId = blog.UserId;
                return View(blogViewModel);
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult UpsertBlog(CreateBlogViewModel blog)
        {
            if (string.IsNullOrEmpty(blog.Content))
            {
                return View();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var blogInfo = new Blog
                {
                    Title = blog.Title,
                    Header = blog.Header,
                    Content = blog.Content,
                    UserId = userId // Set the UserId property to associate the Blog with the user.
                };
                if (blog.Id == 0 || blog.Id == null)
                {
                    if (userId != null)
                    {
                        blogInfo.CreatedAt = DateTime.Now;
                        _db.Blogs.Add(blogInfo);
                    }
                }
                else
                { 
                    blogInfo.Id = blog.Id;
                    _db.Blogs.Update(blogInfo);
                }
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult YourBlogs()
        {
            return View();
        }
    }
}
