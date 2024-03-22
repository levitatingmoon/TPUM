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
        private IShopInventory shopInventory;

        public Shop(IShopInventory shopInventory)
        {
            this.shopInventory = shopInventory;

        }

    }
}
