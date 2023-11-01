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
            var blogs = _db.Blogs.Include(b => b.User).ToList();
            return View(blogs);
        }
        [Authorize]
        public IActionResult UpsertBlog(int? id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var blogViewModel = new CreateBlogViewModel();

            if (id == null || id == 0)
            {
                return View(blogViewModel);
            }
            else
            {
                var blog = _db.Blogs.FirstOrDefault(u => u.Id == id);
                if (blog != null)
                {
                    if (blog.UserId != userId)
                    {
                        return RedirectToAction("Index");
                    }
                    blogViewModel.Id = blog.Id;
                    blogViewModel.Title = blog.Title;
                    blogViewModel.Header = blog.Header;
                    blogViewModel.Content = blog.Content;
                    blogViewModel.CreatedAt = blog.CreatedAt;
                    blogViewModel.UserId = blog.UserId;
                    return View(blogViewModel);
                }
                else
                {
                    return RedirectToAction("Index");
                }
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
                    blogInfo.CreatedAt = blog.CreatedAt;
                    //SET CREATED AT
                    _db.Blogs.Update(blogInfo);
                }
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [Authorize]
        public IActionResult YourBlogs()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Blog> blogs = _db.Blogs.Where(u => u.UserId == userId).Include(b => b.User).ToList();

            return View(blogs);
        }

        public IActionResult ViewBlog(int id)
        {
            var blogViewMoodel = new BlogViewModel
            {
                blog = _db.Blogs.Include(b => b.User).FirstOrDefault(u => u.Id == id),
                addComment = new AddCommentViewModel(),
            };
            blogViewMoodel.blog.Comments = _db.Comments.Where(u=>u.BlogId == blogViewMoodel.blog.Id).ToList();

            if (blogViewMoodel.blog == null)
            {
                return RedirectToAction("Index");
            }
            return View(blogViewMoodel);
        }

        #region API CALLS

        [HttpDelete]
        public IActionResult DeleteBlog(int? id)
        {
            var blogToBeDeleted = _db.Blogs.FirstOrDefault(b => b.Id == id);
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (blogToBeDeleted == null || blogToBeDeleted.UserId != userId)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            else
            {
                _db.Blogs.Remove(blogToBeDeleted);
                _db.SaveChanges();
                return Json(new { success = true, message = "Blog deleted" });
            }
        }
        [HttpPost]
        public IActionResult AddComment([FromBody] AddCommentViewModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = commentModel.Content,
                    CreatedAt = DateTime.Now,
                    UserId = commentModel.UserId,
                    BlogId = commentModel.BlogId,
                };
                _db.Comments.Add(comment);
                _db.SaveChanges();
                return Json(new { success = true, message = "comment added" });
            }
            return Json(new { success = false, message = "comment error" });
        }
        #endregion
    }

}
