using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataLayer
    {
        IStorage Storage { get; set; }

        static IDataLayer Create(IStorage storage = default)
        {
            return new DataLayer(storage);
        }
    }
}
