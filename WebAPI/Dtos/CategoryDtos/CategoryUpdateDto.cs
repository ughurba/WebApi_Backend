using Microsoft.AspNetCore.Http;

namespace WebAPI.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
    }
}
