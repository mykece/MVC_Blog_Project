﻿using HS14MVC.Applicationn.DTOs.ArticleDTOs;
using HS14MVC.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS14MVC.Applicationn.Services.ArticleServices
{
    public interface IArticleService
    {
        Task<IDataResult<List<ArticleListDTO>>> GetAllAsync();
        Task<IDataResult<List<ArticleListDTO>>> GetAllAsync(Guid authorId);
        Task<IDataResult<ArticleDTO>> AddAsync(ArticleCreateDTO articleCreateDTO);
        Task<IDataResult<ArticleDTO>> GetByIdAsync(Guid id);
        Task<IResult> DeleteAsync(Guid id, Guid authorId);
        Task UpViewCountByArticleId(Guid articleId);
        Task<IDataResult<List<ArticleListDTO>>> Top5GetAllAsync();
        Task<IDataResult<List<ArticleListDTO>>> GetAllBySubjectIdAsync(Guid subjectId);
        Task<IDataResult<ArticleDTO>> UpdateAsync(ArticleUpdateDTO articleUpdateDTO);
    }
}
