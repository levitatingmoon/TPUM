using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataLayer
    {
        IShopInventory shopInventory { get; set; }

        static IDataLayer Create(IShopInventory inventory = default)
        {
            return new DataLayer(inventory);
        }
    }
}
