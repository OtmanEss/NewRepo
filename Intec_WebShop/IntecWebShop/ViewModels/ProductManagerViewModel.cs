using IntecWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntecWebShop.ViewModels
{
    public  class ProductManagerViewModel
    {
        // combinaison de deux classes 
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> productCategories { get; set; }
    }
}
