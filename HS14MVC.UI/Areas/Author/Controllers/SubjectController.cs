using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using HS14MVC.Applicationn.DTOs.SubjectDTOs;
using HS14MVC.Applicationn.Services.SubjectServices;
using HS14MVC.UI.Areas.Author.Models.SubjectVMs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace HS14MVC.UI.Areas.Author.Controllers
{
    public class SubjectController : AuthorBaseController
    {
        private readonly ISubjectService _subjectService;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        private readonly IHtmlLocalizer<ModelResource> _htmlLocalizer;

        public SubjectController(ISubjectService subjectService, IStringLocalizer<ModelResource> stringLocalizer, IHtmlLocalizer<ModelResource> htmlLocalizer)
        {
            _subjectService = subjectService;
            _stringLocalizer = stringLocalizer;
            _htmlLocalizer = htmlLocalizer;
        }



        public async Task<IActionResult> Index()
        {
            var result = await _subjectService.GetAllAsync();
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(result.Data.Adapt<List<SubjectListVM>>());
            }
            NotifiySuccess(_stringLocalizer["Success"]);
            return View(result.Data.Adapt<List<SubjectListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubjectCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _subjectService.AddAsync(model.Adapt<SubjectCreateDTO>());
                if (!result.IsSuccess)
                {
                    NotifiyError(result.Messages);
                    return View(model);
                }
                NotifiySuccess(result.Messages);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _subjectService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _subjectService.GetByIdAsync(id);
            var topicUpdateVm = result.Data.Adapt<SubjectUpdateVM>();
            return View(topicUpdateVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubjectUpdateVM model)
        {
            var result = await _subjectService.UpdateAsync(model.Adapt<SubjectUpdateDTO>());
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(model);
            }
            NotifiySuccess(result.Messages);

            return RedirectToAction("Index");
        }
    }
}
