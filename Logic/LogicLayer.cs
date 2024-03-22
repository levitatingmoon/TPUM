using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class LogicLayer : ILogicLayer
    {
        private IDataLayer Datalayer { get; }

        public IShop Shop { get; private set; }

        public LogicLayer(IDataLayer datalayer)
        {
            Datalayer = datalayer;
            Shop = new Shop(Datalayer.shopInventory);
        }
    }
}
