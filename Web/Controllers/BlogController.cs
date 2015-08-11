using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Services;
using Web.Models;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index(Guid id)
        {
            BlogDto blog = _service.GetBlogs().SingleOrDefault(b => b.BlogId == id);

            List<EntryViewModel> entries = new List<EntryViewModel>();
            EntryViewModels entryViewModels = new EntryViewModels();
            
            if (blog != null)
            {
                entries.AddRange(blog.Entries.Select(entry => new EntryViewModel
                {
                    EntryId = entry.EntryId,
                    Body = entry.Body,
                    Title = entry.Title
                }));

                entryViewModels = new EntryViewModels
                {
                    BlogId = blog.BlogId,
                    Entries = entries,
                    Author = blog.Author
                };
            }

            return View(entryViewModels);
        }
    }
}
