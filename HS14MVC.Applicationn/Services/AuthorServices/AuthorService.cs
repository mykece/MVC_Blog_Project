﻿using HS14MVC.Applicationn.DTOs.AuthorDTOs;
using HS14MVC.Applicationn.DTOs.SubjectDTOs;
using HS14MVC.Domain.Entities;
using HS14MVC.Domain.Utilities.Concretes;
using HS14MVC.Domain.Utilities.Interfaces;
using HS14MVC.Infrastructure.Repositories.AuthorRepositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS14MVC.Applicationn.Services.AuthorServices
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IDataResult<AuthorDTO>> AddAsync(AuthorCreateDTO authorCreateDTO)
        {
            if (await _authorRepository.AnyAsync(x=>x.Email == authorCreateDTO.Email))
            {
                return new ErrorDataResult<AuthorDTO>(Messages.Added_Fail);
            }
            var newAuthor = authorCreateDTO.Adapt<Author>();
            await _authorRepository.AddAsync(newAuthor);
            await _authorRepository.SaveChangesAsync();
            return new SuccessDataResult<AuthorDTO>(newAuthor.Adapt<AuthorDTO>(), Messages.Added_Succes);
        }

        public Task<IResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<AuthorListDTO>>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            if (authors.Count() <= 0)
            {
                return new ErrorDataResult<List<AuthorListDTO>>(authors.Adapt<List<AuthorListDTO>>(), "Görüntülenecek Yazar Yok");
            }
            return new SuccessDataResult<List<AuthorListDTO>>(authors.Adapt<List<AuthorListDTO>>(), "Yazar Listeleme Başarılı");
        }

        public async Task<Guid> GetAuthorIdByIdentityId(string identityId)
        {
            var author = await _authorRepository.GetByIdentityId(identityId);
            if (author is null)
            {
                return Guid.Empty;
            }
            return author.Id;
        }

        public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author is null)
            {
                return new ErrorDataResult<AuthorDTO>("Gösterilecek Author Bulunamadı");
            }
            var authorDto = author.Adapt<AuthorDTO>();
            return new SuccessDataResult<AuthorDTO>(authorDto, "Author Gösterme Başarılı");
        }

        public async Task<IDataResult<AuthorDTO>> UpdateAsync(AuthorUpdateDTO authorUpdateDTO)
        {
            var updatingAuthor = await _authorRepository.GetByIdAsync(authorUpdateDTO.Id, false);
            if (updatingAuthor == null)
            {
                return new ErrorDataResult<AuthorDTO>("Güncellenecek Author Bulunamadı");
            }

            var updatedAuthor = authorUpdateDTO.Adapt(updatingAuthor);
            await _authorRepository.UpdateAsync(updatedAuthor);
            await _authorRepository.SaveChangesAsync();


            return new SuccessDataResult<AuthorDTO>(updatedAuthor.Adapt<AuthorDTO>(), "Author Güncelleme Başarılı");
        }
    }
}
