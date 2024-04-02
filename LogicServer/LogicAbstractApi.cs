using DataServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServer
{
    public abstract class LogicAbstractApi
    {
        public abstract IShop Shop { get; }

        public static LogicAbstractApi Create(DataAbstractApi? layerData = default(DataAbstractApi))
        {
            return new LogicLayer(layerData ?? DataAbstractApi.Create());
        }

    }
}
