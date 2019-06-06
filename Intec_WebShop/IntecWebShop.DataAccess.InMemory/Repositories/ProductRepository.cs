using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using IntecWebShop.Models;

namespace IntecWebShop.DataAccess.InMemory.Repositories
{
    // classe qui contient toutes les fonctionnalites de product
    // add reference system.runtime.caching.
    public class ProductRepository
    {
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products==null)
            {
                products = new List<Product>();
            }
        }

        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            // si produit n'est pas null on le remplace par produit
            if (productToUpdate!=null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found!");
            }

        }

        public Product Find(string id)
        {
            Product product = products.Find(p => p.Id == id); 
            if (product!=null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
    }
}
