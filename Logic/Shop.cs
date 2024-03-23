using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class Shop : IShop 
    {
        private IStorage shopInventory;

        public Shop(IStorage shopInventory)
        {
            this.shopInventory = shopInventory;

        }

    }
}
