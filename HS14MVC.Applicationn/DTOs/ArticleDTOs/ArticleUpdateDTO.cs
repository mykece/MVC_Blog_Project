﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS14MVC.Applicationn.DTOs.ArticleDTOs
{
    public class ArticleUpdateDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[]? Image { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
