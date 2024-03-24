using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataLayer : IDataLayer
    {
        public IStorage Storage { get; set; }

        public static DataLayer Create()
        {
            return new DataLayer();
        }

        internal DataLayer(IStorage storage = default)
        {
            if(storage == null)
            {
                this.Storage = new Storage();
            } else
            {
                this.Storage = storage;
            }
            
        }
    }
}
