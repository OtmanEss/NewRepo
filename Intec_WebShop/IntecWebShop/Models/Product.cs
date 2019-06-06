using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntecWebShop.Models
{
    public class Product
    {
        public string Id { get; set; }

        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        //[Range(0.00,1000.00)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }


        // a chaque new product on crée un nouvel id
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
