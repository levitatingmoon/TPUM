﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IShopItem
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }
    }

    internal class ShopItem : IShopItem
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
