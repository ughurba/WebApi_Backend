using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public int Price{ get; set; }
        public int Discount{ get; set; }
        public IFormFile Photo { get; set; }

        public class ProductCreateDtoValidatio : AbstractValidator<ProductCreateDto>
        {
            public ProductCreateDtoValidatio()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("bosh qoyma").MaximumLength(10).WithMessage("10dan artiq ");
                RuleFor(x => x.Price).GreaterThan(50).WithMessage("50 boyuk olmalidi");
                RuleFor(x => x.Photo).NotEmpty();
                RuleFor(p=>p).Custom((p, context) =>
                {
                if (p.Price < p.Discount)
                    {
                        context.AddFailure("Price","price dicountpricedan kicik ola bilmez");
                    }
                });
            }
        }
    }
}
