﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetCategoryById(int id);
        void Add(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}