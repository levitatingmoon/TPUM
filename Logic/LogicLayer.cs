using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class LogicLayer : LogicAbstractApi
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
