﻿using IntecWebShop.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntecWebShop.Core.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
    }
}
