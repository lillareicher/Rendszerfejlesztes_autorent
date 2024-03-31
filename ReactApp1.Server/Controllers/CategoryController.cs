using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
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

        [HttpGet]
        public async Task<IActionResult> ListCategories()
        {
            List<CategoryModel> categories = await _categoryService.ListCategories();

            return Ok(categories);
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> FilterCategories(string categoryName)
        {
            var list = await _categoryService.FilterCategories(categoryName);

            return Ok(list);
        }
    }
}