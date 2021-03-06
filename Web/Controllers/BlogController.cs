﻿using System.Collections.Generic;
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
        public ActionResult Index(int id)
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
                    Title = entry.Title,
                    Date = entry.Date
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
        public ActionResult AddBlog()
        {
            BlogViewModel viewModel = new BlogViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddBlog(BlogViewModel model)
        {
            BlogDto blog = new BlogDto
            {
                Author = model.Author
            };

            _service.AddBlog(blog);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveBlog(int id)
        {
            _service.RemoveBlog(id);

            return RedirectToAction("Index", "Home");
        }
    }
}
