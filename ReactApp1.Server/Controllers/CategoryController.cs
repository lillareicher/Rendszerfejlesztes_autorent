using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Services;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[Authorize(Roles = $"Admin, User")]
        [HttpGet]
        public async Task<IActionResult> ListCategories()
        {
            List<Category> categories = await _categoryService.ListCategories();

            return Ok(categories);
        }

        //[Authorize(Roles = $"Admin, User")]
        [HttpGet("{categoryName}")]
        public async Task<IActionResult> FilterCategories(string categoryName)
        {
            var list = await _categoryService.FilterCategories(categoryName);

            return Ok(list);
        }
    }
}