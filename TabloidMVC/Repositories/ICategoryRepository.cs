using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetCategoryById(int id);
        Category GetUnassignedCategory();
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}