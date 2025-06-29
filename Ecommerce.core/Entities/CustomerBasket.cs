﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {

        }

        public CustomerBasket(int id)
        {
           Id= id;
        }
        public int Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
