using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAPI.Data;
using WebAPI.Dtos.CategoryDtos;
using WebAPI.Extentions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //2.Category entity elave edib CRUD-u tamamliyin(sadece category) ve categoryconfiguration-ni ve dtolari yazin
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpGet]
        public IActionResult GetAll()
        {

            List<CategoryDto> dbCategories = _context.categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                imgUrl = "http://localhost:35319/img/" + c.ImgUrl
            }).ToList();
            return Ok(dbCategories);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Category category = _context.categories.FirstOrDefault(c => c.Id == id);
            CategoryDto categoryDto = new CategoryDto
            {
                Name = category.Name,
            };
            return Ok(categoryDto);
        }
        [HttpPost]
        public IActionResult Create([FromForm] CategoryCreateDto categoryCreateDto)
        {
            bool exist = _context.categories.Any(c => c.Name.ToLower() == categoryCreateDto.Name.ToLower());
            if (exist)
            {
                return NotFound();
            }

            if (!categoryCreateDto.Photo.IsImage())
            {
                return BadRequest();
            }
            if (!categoryCreateDto.Photo.ValidSize(50))
            {
                return BadRequest();
            }
            Category category = new Category
            {
                ImgUrl = categoryCreateDto.Photo.SaveImage(_env, "img"),
                Name = categoryCreateDto.Name
            };
            _context.categories.Add(category);
            _context.SaveChanges();
            return StatusCode(200);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] CategoryUpdateDto CategoryUpdateDto)
        {
            Category category = _context.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            if (_context.categories.Any(c => c.Name.ToLower() == CategoryUpdateDto.Name.ToLower() && c.Id != id))
            {
                return BadRequest();
            }
            if(CategoryUpdateDto.Photo != null)
            {
                if (!CategoryUpdateDto.Photo.IsImage())
                {
                    return BadRequest();
                }
                if (!CategoryUpdateDto.Photo.ValidSize(10))
                {
                    return BadRequest();
                }
                string path = Path.Combine(_env.WebRootPath, "img", category.ImgUrl);
                Helper.Helper.DeleteImage(path);
                category.ImgUrl = CategoryUpdateDto.Photo.SaveImage(_env, "img");

            }
            category.Name = CategoryUpdateDto.Name;
            _context.SaveChanges();
            return Ok("");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Category category = _context.categories.FirstOrDefault(c => c.Id == id);
            _context.categories.Remove(category);
            _context.SaveChanges();
            return StatusCode(200);

        }

    }
}
