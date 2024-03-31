using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IConnectionService
    {
        public Action<string> ConnectionLogger { get; set; }

        public bool Connected { get; }
        public Task<bool> Connect(Uri peerUri);
        public Task Disconnect();
    }
}
