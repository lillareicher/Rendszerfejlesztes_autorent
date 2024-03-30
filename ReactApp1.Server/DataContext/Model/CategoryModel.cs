using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.DataContext.Model
{
    public class CategoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryList
    {
        public List<CategoryModel> Categories { get; set; }
    }
}
