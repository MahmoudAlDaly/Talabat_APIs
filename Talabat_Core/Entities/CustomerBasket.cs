﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class CustomerBasket
	{
        public string ID { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}