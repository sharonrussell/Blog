using System.Web.Mvc;
using Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
