using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataLayer : IDataLayer
    {
        public IStorage shopInventory { get; set; }

        public static DataLayer Create()
        {
            return new DataLayer();
        }

        internal DataLayer(IStorage inventory = default)
        {
            if(inventory == null)
            {
                this.shopInventory = new Storage();
            } else
            {
                this.shopInventory = inventory;
            }
            
        }
    }
}
