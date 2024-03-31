using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ReactApp1.Server.Services
{
    public interface ICategoryService // interface 
    {
        Task<List<CategoryModel>> ListCategories();
        Task<List<CategoryModel>> FilterCategories(string categoryName);
    }

    public class CategoryService : ICategoryService
    {
        public CategoryService() { }

        public async Task<List<CategoryModel>> ListCategories()
        {
            var categories = new List<CategoryModel>();
            //string text = File.ReadAllText(@"./Categories.json");
            //var category = JsonSerializer.Deserialize<CategoryModel>(text);
            CategoryModel cat1 = new CategoryModel() {Id = "ct1", Name = "Race"};
            CategoryModel cat2 = new CategoryModel() {Id = "ct2", Name = "Off-road"};
            CategoryModel cat3 = new CategoryModel() {Id = "ct3", Name = "City"};
            CategoryModel cat4 = new CategoryModel() {Id = "ct4", Name = "Cabrio"};

            categories.Add(cat1);
            categories.Add(cat2);
            categories.Add(cat3);
            categories.Add(cat4);

            return categories;
        }

        public async Task<List<CategoryModel>> FilterCategories (string categoryName)
        {
            List<CategoryModel> categories = await ListCategories();
            List<CategoryModel> filteredCategories = new List<CategoryModel>();

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