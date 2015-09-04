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
        public ActionResult AddEntry(int id)
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

        public ActionResult RemoveEntry(int blogId, int entryId)
        {
            _service.RemoveEntry(blogId, entryId);

            return RedirectToAction("Index", "Blog", new { id = blogId });
        }

        [HttpGet]
        public ActionResult EditEntry(int entryId)
        {
            EntryDto entryDto = _service.GetEntry(entryId);

            EntryViewModel viewModel = new EntryViewModel
            {
                BlogId = entryDto.BlogId,
                EntryId = entryDto.EntryId,
                Body = entryDto.Body,
                Title = entryDto.Title,
                Date = entryDto.Date
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditEntry(EntryViewModel viewModel, int blogId)
        {
            EntryDto entryDto = new EntryDto
            {
                EntryId = viewModel.EntryId,
                Body = viewModel.Body,
                Title = viewModel.Title,
            };

            _service.EditEntry(entryDto);

            return RedirectToAction("Index", "Blog", new { id = blogId });
        }
    }
}
