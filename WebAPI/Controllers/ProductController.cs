using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _context.Products.Where(p => p.isActive).ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == id && p.isActive);
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201);
        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (p == null)
            {
                return NotFound();
            }
            p.Name = product.Name;
            p.Price = product.Price;
            p.isActive = product.isActive;
            _context.SaveChanges();

            return StatusCode(200);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product p = _context.Products.FirstOrDefault(p=>p.Id==id);
            if (p == null)
            {
                return NotFound();
            }
            _context.Products.Remove(p);
            _context.SaveChanges();
            return StatusCode(200);
        }
    }
}
