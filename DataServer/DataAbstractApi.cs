using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    public abstract class DataAbstractApi
    {
        public abstract IStorage Storage { get; set; }

        public static DataAbstractApi Create()
        {
            return new DataApi();
        }
    }
}
