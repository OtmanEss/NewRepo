using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using IntecWebShop.Models;

namespace IntecWebShop.DataAccess.InMemory.Repositories
{
    public class ProductCategoryRepository
    {
        public ProductCategoryRepository()
        {  
            categories = cache["productCategories"] as List<ProductCategory>;
            if (categories == null)
            {
                categories = new List<ProductCategory>();
            }
        }

        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> categories;


        public void Commit()
        {
            cache["productCategories"] = categories;
        }

        public void Insert(ProductCategory category)
        {
            categories.Add(category);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory productToUpdate = categories.Find(p => p.Id == category.Id);

            // si produit n'est pas null on le remplace par produit
            if (productToUpdate != null)
            {
                int index = categories.FindIndex(p => p.Id == category.Id);        // chercher index produit a modifier
                categories[index] = category;                                      // remplace par new product
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public ProductCategory FindProduct(string id)
        {
            ProductCategory category = categories.Find(p => p.Id == id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return categories.AsQueryable();
        }

        public bool Delete(string id)
        {
            ProductCategory productToDelete = FindProduct(id);
            categories.Remove(productToDelete);
            return true;
        }
    }
}
