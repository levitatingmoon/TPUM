using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServer
{
    public interface IDiscount
    {
        Tuple<Guid, float> GetDiscount();
    }
}
