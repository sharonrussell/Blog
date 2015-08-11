using System;
using System.Web.Mvc;
using Services;
using Web.Models;

namespace Web.Controllers
{
    public class EntryController : Controller
    {
        private readonly IEntryService _service;

        public EntryController(IEntryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult AddEntry(Guid id)
        {
            var viewModel = new EntryViewModel
            {
                BlogId = id
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEntry(EntryViewModel model)
        {
            _service.AddEntry(model.BlogId, model.Title, model.Body);

            return RedirectToAction("Index", "Blog", new { id = model.BlogId });
        }

        public ActionResult RemoveEntry(Guid blogId, Guid entryId)
        {
            _service.RemoveEntry(blogId, entryId);

            return RedirectToAction("Index", "Blog", new { id = blogId });
        }
    }
}
