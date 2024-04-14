using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Models.Model;

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
            //string text = File.ReadAllText(@"./Categories.json");
            //var category = JsonSerializer.Deserialize<CategoryModel>(text);
            //Category cat1 = new Category() {Id = "ct1", Name = "Race"};
            //Category cat2 = new Category() {Id = "ct2", Name = "Off-road"};
            //Category cat3 = new Category() {Id = "ct3", Name = "City"};
            //Category cat4 = new Category() {Id = "ct4", Name = "Cabrio"};

            //categories.Add(cat1);
            //categories.Add(cat2);
            //categories.Add(cat3);
            //categories.Add(cat4);

            return categories;
        }

        public async Task<List<Category>> FilterCategories (string categoryName)
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