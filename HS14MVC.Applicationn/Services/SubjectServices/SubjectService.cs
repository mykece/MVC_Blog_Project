﻿using HS14MVC.Applicationn.DTOs.SubjectDTOs;
using HS14MVC.Domain.Entities;
using HS14MVC.Domain.Utilities.Concretes;
using HS14MVC.Domain.Utilities.Interfaces;
using HS14MVC.Infrastructure.Repositories.SubjectRepositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS14MVC.Applicationn.Services.SubjectServices
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IDataResult<SubjectDTO>> AddAsync(SubjectCreateDTO subjectCreateDTO)
        {
            if (await _subjectRepository.AnyAsync(x=>x.Name.ToLower() == subjectCreateDTO.Name.ToLower()))
            {
                return new ErrorDataResult<SubjectDTO>("Konu Sistemde Kayıtlı");
            }
            var newSubject = subjectCreateDTO.Adapt<Subject>();
            await _subjectRepository.AddAsync(newSubject);
            await _subjectRepository.SaveChangesAsync();
            return new SuccessDataResult<SubjectDTO>(newSubject.Adapt<SubjectDTO>(), "Konu Ekleme Başarılı");
        }

        public Task<IResult> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<SubjectListDTO>>> GetAllAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            if (subjects.Count() <= 0)
            {
                return new ErrorDataResult<List<SubjectListDTO>>(subjects.Adapt<List<SubjectListDTO>>(), "Listelenecek Konu Bulunmamakta");
            }

            return new SuccessDataResult<List<SubjectListDTO>>(subjects.Adapt<List<SubjectListDTO>>(), "Konu Listeleme Başarılı");
        }

        public async Task<IDataResult<SubjectDTO>> GetByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject is null)
            {
                return new ErrorDataResult<SubjectDTO>("Gösterilecek Subject Bulunamadı");
            }
            var subjectDto = subject.Adapt<SubjectDTO>();
            return new SuccessDataResult<SubjectDTO>(subjectDto, "Subject Gösterme Başarılı");
        }

        public async Task<IDataResult<SubjectDTO>> UpdateAsync(SubjectUpdateDTO subjectUpdateDTO)
        {
            var updatingSubject = await _subjectRepository.GetByIdAsync(subjectUpdateDTO.Id, false);
            if (updatingSubject == null)
            {
                return new ErrorDataResult<SubjectDTO>("Güncellenecek Subject Bulunamadı");
            }

            var updatedSubject = subjectUpdateDTO.Adapt(updatingSubject);
            await _subjectRepository.UpdateAsync(updatedSubject);
            await _subjectRepository.SaveChangesAsync();


            return new SuccessDataResult<SubjectDTO>(updatedSubject.Adapt<SubjectDTO>(), "Subject Güncelleme Başarılı");
        }
    }
}
