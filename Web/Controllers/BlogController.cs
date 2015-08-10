using System;
using System.Collections;
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

        [HttpGet]
        public ActionResult AddEntry(Guid id)
        {
            BlogDto blog = _service.GetBlogs().SingleOrDefault(b => b.BlogId == id);

            EntryViewModel viewModel = null;

            if (blog != null)
            {
                viewModel = new EntryViewModel
                {
                    BlogId = blog.BlogId
                };
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEntry(EntryViewModel model)
        {
            _service.AddEntry(model.BlogId, model.Title, model.Body);

            return RedirectToAction("Index", "Blog", new {id = model.BlogId});
        }
    }
}
