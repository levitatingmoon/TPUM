using DataServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServer
{
    internal class LogicLayer : LogicAbstractApi
    {
        private DataAbstractApi Datalayer { get; }

        public override IShop Shop { get; }

        public LogicLayer(DataAbstractApi datalayer)
        {
            Datalayer = datalayer;
            Shop = new Shop(Datalayer.Storage);
        }
    }
}
