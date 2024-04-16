using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;

namespace ReactApp1.Server.Services
{
    public interface ICategoryService // interface 
    {
        Task<List<Category>> ListCategories();
        Task<List<Category>> FilterCategories(string categoryName);
    }

    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Category>> ListCategories()
        {
            var categories = await _context.Category.ToListAsync();
            return categories;
        }

        public async Task<List<Category>> FilterCategories(string categoryName)
        {
            List<Category> categories = await ListCategories();
            List<Category> filteredCategories = new List<Category>();

            foreach (var category in categories)
            {
                if (categoryName == category.Name)
                {
                    filteredCategories.Add(category);
                }
            }

            return filteredCategories;
        }
    }


}