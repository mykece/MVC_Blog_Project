using AspNetCoreHero.ToastNotification.Notyf;
using HS14MVC.Applicationn.DTOs.ArticleDTOs;
using HS14MVC.Applicationn.Services.ArticleServices;
using HS14MVC.Applicationn.Services.AuthorServices;
using HS14MVC.Applicationn.Services.SubjectServices;
using HS14MVC.Domain.Utilities.Concretes;
using HS14MVC.UI.Areas.Author.Models.ArticleVMs;
using HS14MVC.UI.Extentions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HS14MVC.UI.Areas.Author.Controllers
{
    public class ArticleController : AuthorBaseController
    {
        private readonly IAuthorService _authorService;
        private readonly IArticleService _articleService;
        private readonly ISubjectService _subjectService;

        public ArticleController(IAuthorService authorService, IArticleService articleService, ISubjectService subjectService)
        {
            _authorService = authorService;
            _articleService = articleService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var authorId = await _authorService.GetAuthorIdByIdentityId(userId);
            var result = await _articleService.GetAllAsync(authorId);
            var articleListVms = result.Data.Adapt<List<ArticleListVM>>();
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(articleListVms);
            }
            NotifiySuccess(result.Messages);
            return View(articleListVms);
        }


        public async Task<IActionResult> Create()
        {
            var createVM = new ArticleCreateVM()
            {
                Subjects = await GetSubjects()
            };
            return View(createVM);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var authorId = await _authorService.GetAuthorIdByIdentityId(userId);
            var result = await _articleService.DeleteAsync(id,authorId);
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Subjects = await GetSubjects();
                return View(model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var authorId = await _authorService.GetAuthorIdByIdentityId(userId);
            var articleCreateDto = model.Adapt<ArticleCreateDTO>();
            articleCreateDto.ReadTime = await model.Content.CalculeTimeForString();
            articleCreateDto.AuthorId = authorId;
            if (model.NewImage !=null && model.NewImage.Length>0)
            {
                articleCreateDto.Image = await model.NewImage.StringToByteArrayAsync();
            }
            var result = await _articleService.AddAsync(articleCreateDto);
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                model.Subjects = await GetSubjects();
                return View(model);
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(Guid id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var articleUpdateVm = article.Data.Adapt<ArticleUpdateVM>();
            articleUpdateVm.Subjects = await GetSubjects();

            // Mevcut resmi view modeline base64 formatında ekliyoruz
            if (article.Data.Image != null && article.Data.Image.Length > 0)
            {
                articleUpdateVm.ExistingImage = Convert.ToBase64String(article.Data.Image);
            }

            return View(articleUpdateVm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, ArticleUpdateVM model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Kullanıcı kimliğini al
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var authorId = await _authorService.GetAuthorIdByIdentityId(userId);

                    if (authorId == Guid.Empty)
                    {
                        ModelState.AddModelError("", "Geçersiz yazar kimliği.");
                        model.Subjects = await GetSubjects(model.SubjectId);
                        return View(model);
                    }

                    var articleUpdateDTO = model.Adapt<ArticleUpdateDTO>();
                    articleUpdateDTO.AuthorId = authorId; // Doğru AuthorId'yi ekliyoruz

                    if (model.NewImage != null && model.NewImage.Length > 0)
                    {
                        articleUpdateDTO.Image = await model.NewImage.StringToByteArrayAsync();
                    }
                    else if (!string.IsNullOrEmpty(model.ExistingImage))
                    {
                        articleUpdateDTO.Image = Convert.FromBase64String(model.ExistingImage);
                    }
                    var article = await _articleService.GetByIdAsync(articleUpdateDTO.Id);
                    if(articleUpdateDTO.Image !=null && articleUpdateDTO.Image.Length > 0)
                    {
                        article.Data.Image = articleUpdateDTO.Image;
                    }
                    var result = await _articleService.UpdateAsync(articleUpdateDTO);

                    if (!result.IsSuccess)
                    {
                        NotifiyError(result.Messages);
                        model.Subjects = await GetSubjects(model.SubjectId);
                        return View(model);
                    }
                    NotifiySuccess(result.Messages);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Hata mesajını ve iç istisnayı logluyoruz
                    var errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += " Inner exception: " + ex.InnerException.Message;
                    }
                    ModelState.AddModelError("", "Güncelleme işlemi sırasında bir hata oluştu: " + errorMessage);
                    model.Subjects = await GetSubjects(model.SubjectId);
                    return View(model);
                }
            }

            model.Subjects = await GetSubjects(model.SubjectId);
            return View(model);
        }






        private async Task<SelectList> GetSubjects(Guid? subjectId = null)
        {
            var subjects = (await _subjectService.GetAllAsync()).Data;
            return new SelectList(subjects.Select(src => new SelectListItem
            {
                Value = src.Id.ToString(),
                Text = src.Name,
                Selected = src.Id == (subjectId != null ? subjectId.Value : subjectId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }
    }
}
