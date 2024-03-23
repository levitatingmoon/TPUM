using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataLayer
    {
        IStorage shopInventory { get; set; }

        static IDataLayer Create(IStorage inventory = default)
        {
            return new DataLayer(inventory);
        }
    }
}
