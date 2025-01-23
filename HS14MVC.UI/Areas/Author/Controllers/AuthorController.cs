using HS14MVC.Applicationn.DTOs.AuthorDTOs;
using HS14MVC.Applicationn.DTOs.SubjectDTOs;
using HS14MVC.Applicationn.Services.AuthorServices;
using HS14MVC.Applicationn.Services.SubjectServices;
using HS14MVC.UI.Areas.Author.Models.AuthorVMs;
using HS14MVC.UI.Areas.Author.Models.SubjectVMs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HS14MVC.UI.Areas.Author.Controllers
{
    public class AuthorController : AuthorBaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorService.GetAllAsync();
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(result.Data.Adapt<List<AuthorListVM>>());
            }
            NotifiySuccess(result.Messages);
            return View(result.Data.Adapt<List<AuthorListVM>>());
        }

        
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _authorService.DeleteAsync(id);
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
            var result = await _authorService.GetByIdAsync(id);
            var authorUpdateVm = result.Data.Adapt<AuthorUpdateVM>();
            return View(authorUpdateVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AuthorUpdateVM model)
        {
            // Mevcut veritabanı kaydını bul
            var author = await _authorService.GetByIdAsync(model.Id);
            if (author == null)
            {
                
                return View(model);
            }

            // Mevcut Email'i model'e ayarla
            model.Email = author.Data.Email;

            // Modeli DTO'ya dönüştür ve güncelle
            var result = await _authorService.UpdateAsync(model.Adapt<AuthorUpdateDTO>());

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
