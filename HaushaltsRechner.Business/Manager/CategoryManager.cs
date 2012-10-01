using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Manager
{
    public static class CategoryManager
    {
        public static IQueryable<CATEGORY> GetAllCategories()
        {
            var en = new HaushaltsrechnerEntities();
            
            return en.CATEGORY;            
        }

        public static CATEGORY GetCategoryByID(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.CATEGORY.FirstOrDefault(c => c.ID == id);
            }
        }
    }
}