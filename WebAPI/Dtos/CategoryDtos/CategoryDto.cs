﻿using Microsoft.AspNetCore.Http;

namespace WebAPI.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string imgUrl{ get; set; }
    }
}
