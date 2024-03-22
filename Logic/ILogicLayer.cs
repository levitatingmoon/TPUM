using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public interface ILogicLayer
    {
        public IShop Shop { get; }

        public static ILogicLayer Create(IDataLayer? layerData = default(IDataLayer))
        {
            return new LogicLayer(layerData ?? IDataLayer.Create());
        }

    }
}
