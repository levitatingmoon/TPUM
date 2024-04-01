using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ServiceFactory
    {
        public static IConnectionService CreateConnectionService => new ConnectionService();
    }
}
