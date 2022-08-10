using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Data;
using WebAPI.Dtos.CategoryDtos;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //2.Category entity elave edib CRUD-u tamamliyin(sadece category) ve categoryconfiguration-ni ve dtolari yazin
    [Route("api/[controller]")]
    [ApiController]
   
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;

        }


        [HttpGet]
        public IActionResult GetAll()
        {

            List <CategoryDto> dbCategories =  _context.categories.Select(c=>new CategoryDto { Name = c.Name}).ToList();
            return Ok(dbCategories);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Category category = _context.categories.FirstOrDefault(c=>c.Id == id);
            CategoryDto categoryDto = new CategoryDto
            {
                Name = category.Name,
            };
            return Ok(categoryDto);
        }
        [HttpPost]
        public IActionResult Create(CategoryCreateDto categoryCreateDto)
        {
            Category category = new Category
            {
                Name = categoryCreateDto.Name
            };
            _context.categories.Add(category);
            _context.SaveChanges();
            return StatusCode(200);
        }
        [HttpPut]
        public IActionResult Update(int id ,CategoryDto categoryDto) 
        {
            Category category= _context.categories.FirstOrDefault(c=>c.Id == id);
            category.Name = categoryDto.Name;
            _context.SaveChanges();
            return StatusCode(200);
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
