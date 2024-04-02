using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    internal class DataApi : DataAbstractApi
    {
        public override IStorage Storage { get; set; }

        internal DataApi(IStorage storage = default)
        {
            if (storage == null)
            {
                this.Storage = new Storage();
            }
            else
            {
                this.Storage = storage;
            }

        }
    }
}
