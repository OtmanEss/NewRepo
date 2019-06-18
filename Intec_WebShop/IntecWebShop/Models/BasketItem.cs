﻿using IntecWebShop.Core.Models;
using System;
using System.Collections.Generic;

namespace IntecWebShop.Core.Models
{
    public class BasketItem:BaseEntity
    {
        public string BasketId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}