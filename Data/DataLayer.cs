using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataLayer : IDataLayer
    {
        public IShopInventory shopInventory { get; set; }

        public static DataLayer Create()
        {
            return new DataLayer();
        }

        internal DataLayer(IShopInventory inventory = default)
        {
            if(inventory == null)
            {
                this.shopInventory = new ShopInventory();
            } else
            {
                this.shopInventory = inventory;
            }
            
        }
    }
}
