using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string Name{ get; set; }
        public IFormFile Photo{ get; set; }
      
    }

    public class CategoryCreateDtoValidatio : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidatio()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("bosh qoyma").MaximumLength(10).WithMessage("10dan artiq ");
           
            RuleFor(x => x.Photo).NotEmpty();
        }
    }
}
