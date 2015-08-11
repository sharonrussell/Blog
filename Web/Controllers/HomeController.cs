using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Services;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            IEnumerable<BlogDto> blogs = _blogService.GetBlogs();

            IEnumerable<BlogViewModel> blogViewModels = blogs.Select(blog => new BlogViewModel
            {
                BlogId = blog.BlogId, 
                Author = blog.Author
            });

            return View(blogViewModels);
        }
    }
}
