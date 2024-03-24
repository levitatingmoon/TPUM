using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IItem
    {
        string name { get; }
        float price { get; set; }
        Guid id { get; }
        ItemType type { get; }
    }
}
